using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Turkcell.Updater.UnitTests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void TestProductName()
        {
            Configuration.FetchAssemblyInfo();
            Assert.AreEqual("Turkcell.Updater", Configuration.ProductName);
        }


    }
}
