using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    public class TestProperty<T> : IContextProperty, IContributeObjectSink where T : IMessageSink, ITestAspect, new()
    {
        #region Fields

        private readonly string _name = typeof(T).AssemblyQualifiedName;

        #endregion // Fields

        #region IContextProperty Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public void Freeze(Context newContext)
        {
        }

        public string Name
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
            get { return _name; }
        }

        #endregion // IContextProperty Members

        #region IContributeObjectSink Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            T testAspect = new T();
            testAspect.AddMessageSink(nextSink);
            return testAspect;
        }

        #endregion // IContributeObjectSink Members
    }
}
