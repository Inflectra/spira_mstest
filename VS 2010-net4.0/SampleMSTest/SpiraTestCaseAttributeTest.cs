using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension.SampleMSTest
{
    /// <summary>
    /// Sample test fixture that tests the SpiraTest integration
    /// Written by Paul Tissue. Packed by Inflectra Corporation
    /// </summary>
    /// <remarks>
    /// Updated on 8/4/2011 to support VS2010
    /// </remarks>
    [
    TestClass
    ]
    public class SpiraTestCaseAttributeTest : MSTestExtensionsTestFixture
    {
        /// <summary>
        /// Test fixture state
        /// </summary>
        protected static int testFixtureState = 1;

        /// <summary>
        /// Constructor method
        /// </summary>
        public SpiraTestCaseAttributeTest()
        {
            //Delegates to base

            //Set the state to 2
            testFixtureState = 2;
        }

        /// <summary>
        /// Sample test that asserts a failure and overrides the default configuration
        /// </summary>
        [
        TestMethod,
        SpiraTestCase(5),
        SpiraTestConfiguration("http://localhost/SpiraTest", "fredbloggs", "fredbloggs", 1, 1, 2)
        ]
        public void SampleFail()
        {
            //Verify the state
            Assert.AreEqual(2, testFixtureState, "*Real Error*: State not persisted");

            //Thread.Sleep(65000);

            // If you are debugging this, the debugger will halt 
            // and display the exeception, so press F5 to continue

            //Failure Assertion
            Assert.AreEqual(1, 0, "Failed as Expected");
        }

        /// <summary>
        /// Sample test that succeeds - uses the default configuration
        /// </summary>
        [
        TestMethod,
        SpiraTestCase(6)
        ]
        public void SamplePass()
        {
            //Verify the state
            Assert.AreEqual(2, testFixtureState, "*Real Error*: State not persisted");

            //Successful assertion
            Assert.AreEqual(1, 1, "Passed as Expected");
        }

        /// <summary>
        /// Sample test that does not log to SpiraTest
        /// </summary>
        [
        TestMethod,
        ExpectedException(typeof(AssertFailedException))
        ]
        public void SampleIgnore()
        {
            //Verify the state
            Assert.AreEqual(2, testFixtureState, "*Real Error*: State not persisted");

            // If you are debugging this, the debugger will halt 
            // and display the exeception, so press F5 to continue

            //Failure Assertion
            Assert.AreEqual(1, 0, "Failed as Expected");
        }

    }
}
