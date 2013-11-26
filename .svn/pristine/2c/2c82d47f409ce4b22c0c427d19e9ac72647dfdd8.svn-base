using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater.UnitTests
{
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void TestParseIsoDate1()
        {
            var dateTime = DateTimeUtils.ParseIsoDate("1999-06-23 10:23:10.20");
            if (dateTime != null)
            {
                DateTime d = dateTime.Value;
                Assert.AreEqual(d.Year, 1999);
                Assert.AreEqual(d.Month, 6);
                Assert.AreEqual(d.Day, 23);

                Assert.AreEqual(d.Hour, 10);
                Assert.AreEqual(d.Minute, 23);
                Assert.AreEqual(d.Second, 10);
            }
            else
            {
                throw new Exception("Unable to parse");
            }
        }

        [TestMethod]
        public void TestParseIsoDate2()
        {
            var dateTime = DateTimeUtils.ParseIsoDate("1999-06-23 10:23");
            if (dateTime != null)
            {
                DateTime d = dateTime.Value;

                Assert.AreEqual(d.Year, 1999);
                Assert.AreEqual(d.Month, 6);
                Assert.AreEqual(d.Day, 23);

                Assert.AreEqual(d.Hour, 10);
                Assert.AreEqual(d.Minute, 23);
                Assert.AreEqual(d.Second, 0);
            }
            else
            {
                throw new Exception("Unable to parse");
            }
        }

        [TestMethod]
        public void TestParseIsoDate3()
        {
            var dateTime = DateTimeUtils.ParseIsoDate("1999-06-23");
            if (dateTime != null)
            {
                DateTime d = dateTime.Value;

                Assert.AreEqual(d.Year, 1999);
                Assert.AreEqual(d.Month, 6);
                Assert.AreEqual(d.Day, 23);

                Assert.AreEqual(d.Hour, 0);
                Assert.AreEqual(d.Minute, 0);
                Assert.AreEqual(d.Second, 0);
            }
            else
            {
                throw new Exception("Unable to parse");
            }
        }

    }
}
