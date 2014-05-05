using System;

namespace Turkcell.Updater
{
    /// <summary>
    ///     Contains information about the problems preventing library to execute as expected.
    /// </summary>
    public class UpdaterException : Exception
    {
        /// <summary>
        ///     Creates an instance of <see cref="UpdaterException" />
        /// </summary>
        internal UpdaterException()
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="UpdaterException" /> with provided message.
        /// </summary>
        /// <param name="detailMessage"></param>
        internal UpdaterException(String detailMessage)
            : base(detailMessage)
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="UpdaterException" /> using provided exception. You can check the details looking at
        ///     <see
        ///         cref="Exception.InnerException" />
        ///     property.
        /// </summary>
        /// <param name="exc"></param>
        internal UpdaterException(Exception exc)
            : base("Check the inner exception", exc)
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="UpdaterException" /> using provided message and inner exception.
        /// </summary>
        /// <param name="detailMessage">
        ///     <see cref="Exception.Message" />
        /// </param>
        /// <param name="innerException">
        ///     <see cref="Exception.InnerException" />
        /// </param>
        internal UpdaterException(String detailMessage, Exception innerException)
            : base(detailMessage, innerException)
        {
        }
    }
}