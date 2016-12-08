using System;
using System.Runtime.Serialization;

namespace Inflectra.SpiraTest.AddOns.SpiraTestMSTestExtension
{
    /// <summary>
    /// This is the base exception class for the MSTestExtensions class
    /// that all test cases need to derive from
    /// </summary>
    [Serializable]
    public partial class MSTestExtensionsException : Exception
    {
        #region Constructors

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <remarks>
        /// Creates the exception with the <see cref="_defaultExceptionMessage"/>.
        /// </remarks>
        public MSTestExtensionsException()
            : this(_defaultExceptionMessage)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">A <see cref="String"/>. The error message that explains the reason for the exception.</param>
        /// <remarks>
        /// None.
        /// </remarks>
        public MSTestExtensionsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">A <see cref="String"/>. The error message that explains the reason for the exception.</param>
        /// <param name="innerException">An <see cref="Exception"/>. The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        /// <remarks>
        /// None.
        /// </remarks>
        public MSTestExtensionsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/>. The object that holds the serialized object data.</param>
        /// <param name="context">A <see cref="StreamingContext"/>. The contextual information about the source or destination.</param>
        /// <remarks>
        /// None.
        /// </remarks>
        protected MSTestExtensionsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion // Constructors

        #region Fields

        private const string _defaultExceptionMessage = 
            "There has been an exception but no information was provided. Please view the stack trace for more information.";

        #endregion // Fields
    }
}
