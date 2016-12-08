using System;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// This stores the SpiraTest Test Case Id for the specific unit test
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class SpiraTestCaseAttribute : Attribute
    {
        #region Fields

        private int testCaseId;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// The SpiraTest Test Case Id
        /// </summary>
        public int TestCaseId
        {
            get
            {
                return this.testCaseId;
            }
            set
            {
                this.testCaseId = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The constructor is called when the attribute is set.
        /// </summary>
        /// <param name="testCaseId">The SpiraTest Test Case Id</param>
        public SpiraTestCaseAttribute(int testCaseId)
        {
            //Update the local member variables
            this.testCaseId = testCaseId;
        }

        #endregion // Constructors
    }
}
