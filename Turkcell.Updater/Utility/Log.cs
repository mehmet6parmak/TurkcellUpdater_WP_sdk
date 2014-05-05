using System;
using System.Diagnostics;

namespace Turkcell.Updater.Utility
{
    internal class Log
    {
        public static void E(string message, Exception e = null)
        {
            Write("ERROR: " + message);
            if (e != null)
            {
                Write(e.Message);
                Write(e.StackTrace);
            }
        }

        private static void Write(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
            Debugger.Log(0, String.Empty, message + Environment.NewLine);
        }

        public static void I(string message)
        {
            Write("INFO: " + message);
        }

        internal static void PrintProductInfo()
        {
#if DEBUG
            E("Turkcell Updater Debug version of " + Configuration.ProductName + " should not be used in production environment.");
#endif
            I("Turkcell Updater Version: " + Configuration.ProductVersion);
        }

        internal static void D(string message, Exception e = null)
        {
            Write("DEBUG: " + message);
            if (e != null)
            {
                Write(e.Message);
                Write(e.StackTrace);
            }
        }
    }
}