using System;
using System.Net;
using LitJson;

namespace Turkcell.Updater
{
    /// <summary>
    /// This class contains results of an Updater request. 
    /// You can check <see cref="StatusCode"/> for the HTTP result or <see cref="Error"/> if any error occured while processing request. 
    /// Any <see cref="Turkcell.Updater.Message"/> and <see cref="Turkcell.Updater.Update"/>'s are available through <see cref="Message"/> and <see cref="Update"/> properties.
    /// </summary>
    public class TurkcellUpdaterResponse
    {
        /// <summary>
        /// The http status Code for the http request.
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// Errors returned by Turkcell Updater Server or any exceptions occured while processing server responses. Http communication exceptions are not passed to application, you can only check status code via <see cref="StatusCode"/> property.
        /// </summary>
        public UpdaterException Error { get; internal set; }

        internal JsonData Response { get; set; }

        internal TurkcellUpdaterResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        internal TurkcellUpdaterResponse(Exception e)
        {
            Error = new UpdaterException(e);
        }

        /// <summary>
        /// Available <see cref="Message"/> instance for the current application and update configurations.
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        /// Available <see cref="Message"/> instance for the current application and update configurations.
        /// </summary>
        public Update Update { get; set; }
    }
}
