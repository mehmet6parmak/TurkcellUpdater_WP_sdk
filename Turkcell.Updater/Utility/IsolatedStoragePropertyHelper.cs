using System.IO.IsolatedStorage;

namespace Turkcell.Updater.Utility
{
    /// <summary>
    ///     Helper class is needed because IsolatedStorageProperty is generic and
    ///     can not provide singleton model for static content
    /// </summary>
    internal static class IsolatedStoragePropertyHelper
    {
        /// <summary>
        ///     We must use this object to lock saving settings
        /// </summary>
        public static readonly object ThreadLocker = new object();

        public static readonly IsolatedStorageSettings Store = IsolatedStorageSettings.ApplicationSettings;
    }
}