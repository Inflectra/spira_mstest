using System.Runtime.Remoting.Messaging;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// The iterface for all test aspects
    /// </summary>
    public interface ITestAspect
    {
        void AddMessageSink(IMessageSink messageSink);
    }
}
