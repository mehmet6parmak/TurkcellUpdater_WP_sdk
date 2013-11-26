using System;
using System.Reflection;
using Microsoft.Devices;

namespace Turkcell.Updater
{
    /// <summary>
    /// Contains some metadata about the library
    /// </summary>
    public static class Configuration
    {
        internal const int Level = 1;

        /// <summary>
        /// 
        /// </summary>
        public static string ProductName;
        /// <summary>
        /// 
        /// </summary>
        public static Version ProductVersion;

#if DEBUG
        internal static bool Debug = true;
#else
        internal static bool Debug = false;
#endif
	
        /// <summary>
        /// Setting this value to <strong>null</strong> disables MIME type checking for JSON files.
        /// </summary>
	    internal static String ExpectedJsonMimeType = Debug ? null : "application/json";

        static Configuration()
        {
            var currentAssembly = new AssemblyName(Assembly.GetExecutingAssembly().FullName);

            ProductName = currentAssembly.Name;
            ProductVersion = currentAssembly.Version;

            if(!Debug)
                Debug = Microsoft.Devices.Environment.DeviceType == DeviceType.Emulator;
        }

        internal static void FetchAssemblyInfo()
        {
            var currentAssembly = new AssemblyName(Assembly.GetExecutingAssembly().FullName);

            ProductName = currentAssembly.Name;
            ProductVersion = currentAssembly.Version;

            if (!Debug)
                Debug = Microsoft.Devices.Environment.DeviceType == DeviceType.Emulator;
        }
    }
}
