using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary />
    /// <typeparam name="TAttribute">The <see cref="Attribute"/> associated with the Aspect.</typeparam>
    /// <remarks />
    public abstract class TestAspect<TAttribute> where TAttribute : Attribute
    {
        #region Methods

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        protected TAttribute GetAttribute(IMessage message)
        {
            string uri = (string)message.Properties[MessageKeys.Uri];
            string methodName = (string)message.Properties[MessageKeys.MethodName];
            //string methodSignature = (string)message.Properties[MessageKeys.MethodSignature];
            string typeName = (string)message.Properties[MessageKeys.TypeName];
            //string args = (string)message.Properties[MessageKeys.Args];
            //string callContext = (string)message.Properties[MessageKeys.CallContext];

            Type callingType = Type.GetType(typeName);
            MethodInfo methodInfo = callingType.GetMethod(methodName);
            object[] attributes = methodInfo.GetCustomAttributes(typeof(TAttribute), true);
            TAttribute attribute = null;
            if (attributes.Length > 0)
            {
                attribute = attributes[0] as TAttribute;
            }
            return attribute;
        }

        /// <summary>
        /// Gets a specific attribute, not the current aspect's
        /// </summary>
        /// <param name="message"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        protected Attribute GetAttribute(IMessage message, Type attributeType)
        {
            string uri = (string)message.Properties[MessageKeys.Uri];
            string methodName = (string)message.Properties[MessageKeys.MethodName];
            //string methodSignature = (string)message.Properties[MessageKeys.MethodSignature];
            string typeName = (string)message.Properties[MessageKeys.TypeName];
            //string args = (string)message.Properties[MessageKeys.Args];
            //string callContext = (string)message.Properties[MessageKeys.CallContext];

            Type callingType = Type.GetType(typeName);
            MethodInfo methodInfo = callingType.GetMethod(methodName);
            object[] attributes = methodInfo.GetCustomAttributes(attributeType, true);
            Attribute attribute = null;
            if (attributes.Length > 0)
            {
                attribute = (Attribute)attributes[0];
            }
            return attribute;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        protected string GetMethodName(IMessage message)
        {
            return (string)message.Properties[MessageKeys.MethodName];
        }

        protected IMessage FakeTargetResponse(IMessage message)
        {
            IMethodCallMessage methodCallMessage = new MethodCall(message);
            return new MethodResponse(new Header[0], methodCallMessage);
        }

        #endregion // Methods

        #region Structs

        private struct MessageKeys
        {
            public const string Uri = "__Uri";
            public const string MethodName = "__MethodName";
            public const string MethodSignature = "__MethodSignature";
            public const string TypeName = "__TypeName";
            public const string Args = "__Args";
            public const string CallContext = "__CallContext";
        }

        #endregion // Structs
    }
}
