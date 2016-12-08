using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MSTestExtensionsTestAttribute : ContextAttribute
    {
        #region Constructors

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public MSTestExtensionsTestAttribute()
            : base("MSTestExtensionsTest")
        {
        }

        #endregion // Constructors

        #region Methods

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override void GetPropertiesForNewContext(IConstructionCallMessage msg)
        {
            //Make sure we have a valid construction call message
            if (msg == null)
            {
                throw new ArgumentNullException("msg");
            }
            msg.ContextProperties.Add(new TestProperty<SpiraTestCaseAspect>());
            msg.ContextProperties.Add(new TestProperty<SpiraTestConfigurationAspect>());
        }

        #endregion // Methods
    }
}
