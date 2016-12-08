using System;
using System.Reflection;
using System.Configuration;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// The aspect that allows you to specify the SpiraTest Case id of the test case
    /// </summary>
    public class SpiraTestCaseAspect : TestAspect<SpiraTestCaseAttribute>, IMessageSink, ITestAspect
    {
        #region Constants

        private const string CLASS_NAME = "SpiraTestCase::";
        protected const string TEST_EXECUTE_WEB_SERVICES_URL = "/Services/v2_2/ImportExport.asmx";

        internal const string SOURCE_NAME = "SpiraTestMSTestAddIn";
        internal const string TEST_RUNNER_NAME = "MSTest";	//This is the name we pass to SpiraTest

        #endregion // Constants

        protected int testCaseId = 0;

        #region Fields

        private IMessageSink _nextSink;

        #endregion // Fields

        #region IMessageSink Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public IMessage SyncProcessMessage(IMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");

            DateTime timeBeforeTest = DateTime.Now;
            IMessage returnMessage = _nextSink.SyncProcessMessage(msg);
            DateTime timeAfterTest = DateTime.Now;

            //Need to get the SpiraTestCase attribute associated with the test method
            SpiraTestCaseAttribute testMethodAttribute = GetAttribute(msg);
            if (testMethodAttribute != null)
            {
                //See if we have a custom SpiraTest configuration attribute also specified
                SpiraTestConfigurationAttribute testConfigAttribute = (SpiraTestConfigurationAttribute)GetAttribute(msg, typeof(SpiraTestConfigurationAttribute));

                // Get the TestCaseId
                this.testCaseId = testMethodAttribute.TestCaseId;
                TestCaseResult result = new TestCaseResult();
                ReturnMessage returnMsg = returnMessage as ReturnMessage;

                // Get the Test Results
                result.Name = GetMethodName(msg);
                result.testCaseName = GetMethodName(msg);
                result.testMethodName = GetMethodName(msg);
                if (returnMsg != null)
                {
                    result.Message = returnMsg.Exception.Message;
                    result.Executed = true;
                    result.IsFailure = true;
                    result.IsSuccess = false;
                    //result.StackTrace = returnMsg.Exception.StackTrace;
                    result.StackTrace = returnMsg.Exception.ToString();
                }
                else
                {
                    result.Message = "Passed";
                    result.Executed = true;
                    result.IsFailure = false;
                    result.IsSuccess = true;
                    result.StackTrace = null;
                }
                result.AssertCount = 0;
                result.Time = (timeAfterTest - timeBeforeTest).TotalSeconds;

                // Log the results in SpiraTest
                bool recordTestRun = Properties.Settings.Default.RecordTestRun;
                if (recordTestRun)
                {
                    Run(testConfigAttribute, result);
                }
            }

            return returnMessage;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }

        public IMessageSink NextSink
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
            get { return _nextSink; }
        }

        #endregion // IMessageSink Members

        #region ITestAspect

        public void AddMessageSink(IMessageSink messageSink)
        {
            _nextSink = messageSink;
        }

        /// <summary>
        /// Executes the test case
        /// </summary>
        /// <param name="result">The test case result</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public void Run(SpiraTestConfigurationAttribute testConfigAttribute, TestCaseResult result)
        {
            const string METHOD_NAME = "Run: ";

            try
            {
                //Get the DefaultConfigPrefix from the config file
                string defaultConfigPrefix = ConfigurationSettings.AppSettings["DefaultConfigPrefix"];

                //Get the URL, Login, Password and ProjectId from the 
                // config file
                string url = Properties.Settings.Default.Url;
                string login = Properties.Settings.Default.Login;
                string password = Properties.Settings.Default.Password;
                int projectId = Properties.Settings.Default.ProjectId;
                Nullable<int> releaseId = null;
                int tempReleaseId;
                if (Int32.TryParse(Properties.Settings.Default.ReleaseId, out tempReleaseId))
                {
                    releaseId = tempReleaseId;
                }
                Nullable<int> testSetId = null;
                int tempTestSetId;
                if (Int32.TryParse(Properties.Settings.Default.TestSetId, out tempTestSetId))
                {
                    testSetId = tempTestSetId;
                }

                //See if we need to override the default configuration settings
                if (testConfigAttribute != null)
                {
                    url = testConfigAttribute.Url;
                    login = testConfigAttribute.Login;
                    password = testConfigAttribute.Password;
                    projectId = testConfigAttribute.ProjectId;
                    releaseId = testConfigAttribute.ReleaseId;
                    testSetId = testConfigAttribute.TestSetId;
                }

                //Now we need to extract the result information
                int executionStatusId = -1;
                if (!result.Executed)
                {
                    //Set status to 'Not Run'
                    executionStatusId = 3;
                }
                else
                {
                    if (result.IsFailure)
                    {
                        //Set status to 'Failed'
                        executionStatusId = 1;
                    }
                    if (result.IsSuccess)
                    {
                        //Set status to 'Passed'
                        executionStatusId = 2;
                    }
                }

                //Extract the other information
                string testCaseName = result.Name;
                string message = result.Message;
                string stackTrace = result.StackTrace;
                int assertCount = result.AssertCount;
                DateTime startDate = DateTime.Now.AddSeconds(-result.Time);
                DateTime endDate = DateTime.Now;

                //Instantiate the web-service proxy class and set the URL from the text box
                bool success = false;
                SpiraImportExport.ImportExport spiraTestExecuteProxy = new SpiraImportExport.ImportExport();
                spiraTestExecuteProxy.Url = url + TEST_EXECUTE_WEB_SERVICES_URL;

                //Create a new cookie container to hold the session handle
                CookieContainer cookieContainer = new CookieContainer();
                spiraTestExecuteProxy.CookieContainer = cookieContainer;

                //Attempt to authenticate the user
                success = spiraTestExecuteProxy.Connection_Authenticate(login, password);
                if (!success)
                {
                    throw new Exception(
                        "Cannot authenticate with SpiraTest, check the URL, login and password");
                }

                //Now connect to the specified project
                success = spiraTestExecuteProxy.Connection_ConnectToProject(projectId);
                if (!success)
                {
                    throw new Exception(
                        "Cannot connect to the specified project, check permissions of user!");
                }

                //Now actually record the test run itself
                SpiraImportExport.RemoteTestRun remoteTestRun = new SpiraImportExport.RemoteTestRun();
                remoteTestRun.TestCaseId = testCaseId;
                remoteTestRun.ReleaseId = releaseId;
                remoteTestRun.TestSetId = testSetId;
                remoteTestRun.StartDate = startDate;
                remoteTestRun.EndDate = endDate;
                remoteTestRun.ExecutionStatusId = executionStatusId;
                remoteTestRun.RunnerName = TEST_RUNNER_NAME;
                remoteTestRun.RunnerTestName = testCaseName;
                remoteTestRun.RunnerAssertCount = assertCount;
                remoteTestRun.RunnerMessage = message;
                remoteTestRun.RunnerStackTrace = stackTrace;
                spiraTestExecuteProxy.TestRun_RecordAutomated1(remoteTestRun);

                //Close the SpiraTest connection
                spiraTestExecuteProxy.Connection_Disconnect();
            }
            catch (Exception exception)
            {
                //Log error then rethrow
                System.Diagnostics.EventLog.WriteEntry(
                    SOURCE_NAME,
                    CLASS_NAME + METHOD_NAME + exception.Message,
                    System.Diagnostics.EventLogEntryType.Error);
                throw exception;
            }
        }

        #endregion // ITestAspect
    }
}
