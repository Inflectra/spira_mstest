using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// Stores the set of SpiraTest test case results
    /// </summary>
    public class TestCaseResult
    {
        public string Name;
        public string testCaseName;
        public string testMethodName;
        public string Message;
        public string StackTrace;
        public int AssertCount = 0;
        public bool Executed = true;
        public bool IsFailure = false;
        public bool IsSuccess = true;
        public double Time;

    }
}
