using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Turkcell.Updater.UnitTests
{
    [TestClass]
    public class PropertiesTests
    {
        [TestMethod]
        public void TestPropertiesContext()
        {
            var task = Properties.CreateInstance();
            task.Wait();
            var properties = task.Result;

            Assert.AreEqual("{3f0f82fd-e012-4eb7-80ba-a8d06bc9343a}", properties[Properties.KeyAppPackageId]);
            Assert.AreEqual("1.0.0.0", properties[Properties.KeyAppVersion]);
            Assert.AreEqual(Environment.OSVersion.Version.ToString(), properties[Properties.KeyDeviceOsVersion]);
        }

    }
}
