using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// This is the base class for all test cases that integrate with SpiraTest
    /// </summary>
    /// <remarks>You need to have your test fixture derive from this class</remarks>
    [MSTestExtensionsTest]
    public class MSTestExtensionsTestFixture : ContextBoundObject
    {
        private const string CLASS_NAME = "MSTestExtensionsTestFixture::";
        internal const string SOURCE_NAME = "MSTestExtensionsTestFixture";
        internal const string TEST_RUNNER_NAME = "MSTest";	//This is the name we pass to SpiraTest

        /// <summary>
        /// Test context instance
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; } // get
            set { testContextInstance = value; } // set
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        /// <summary>
        /// Use ClassInitialize to run code before running the first test in the class
        /// </summary>
        /// <param name="testContext">Test context</param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            const string METHOD_NAME = "ClassInitialize: ";

            try
            {
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

        /// <summary>
        /// Use ClassCleanup to run code after all tests in a class have run
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            const string METHOD_NAME = "ClassCleanup: ";

            try
            {
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

        /// <summary>
        /// Use TestInitialize to run code before running each test
        /// </summary>
        [TestInitialize()]
        public void TestInitialize()
        {
            const string METHOD_NAME = "TestInitialize: ";

            try
            {
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

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TestCleanup()]
        public void TestCleanup()
        {
            const string METHOD_NAME = "TestCleanup: ";

            try
            {
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

        #endregion
    }
}
