using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LitJson;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Turkcell.Updater.UnitTests
{
    [TestClass]
    public class MessageEntryTests
    {
        static String update = "{\"packageName\": \"{1fa4c550-7e08-4dae-b3e2-bd2ab24761f3}\",\n\t\n\t\"messages\": [\n\t\t{\n\t\t\t\"descriptions\": {\n\t\t\t\t\"*\": {\n\t\t\t\t\t\"title\": \"Offer\",\n\t\t\t\t\t\"message\": \"New application is available!!!\",\n\t\t\t\t\t\"imageUrl\": \"http://example.com/app2-icon.png\"\n\t\t\t\t}\n\t\t\t},\n\t\t\t\"targetWebsiteUrl\": \"https://itunes.apple.com/us/app/cuzdan/id562695538?mt=8\",\n\t\t\t\"maxDisplayCount\": 3\n\t\t}\n\t]\n\n}";


        private static MessageEntry CreateMessageEntry(String json)
        {

            JsonMapper.Init();
            var jsonData = JsonMapper.ToObject(json);
            return new MessageEntry(jsonData);
        }

        private static MessageEntry CreateSample1()
        {
            return CreateMessageEntry("{" + "   \"descriptions\":{	\"*\":\"message\"" + "	}" + "}");
        }

        private MessageDisplayRecords createRecords()
        {
            var result = new MessageDisplayRecords();
#pragma warning disable 612,618
            result.DeleteMessageRecords();
#pragma warning restore 612,618
            return result;
        }

        private Properties createProperties()
        {
            var pm = new Dictionary<String, String>
                {
                    {Properties.KeyAppVersion, "3"},
                    {Properties.KeyDeviceIsTablet, "true"},
                    {Properties.KeyDeviceLanguage, "en"},
                    {Properties.KeyCustomPrefix + "costum-key-1", "costum-key-1"},
                    {Properties.KeyCustomPrefix + "costum-key-2", "costum-key-2"},
                    {Properties.KeyCustomPrefix + "costum-key-3", "costum-super-key"}
                };
            Task<Properties> task = Properties.CreateInstance(pm);
            task.Wait();

            return task.Result;
        }

        [TestMethod]
        public void TestShouldDisplay1()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            MessageEntry me = CreateSample1();
            Assert.IsNotNull(me);

            // id should be auto generated
            Assert.IsTrue(me.Id != 0);

            // always should be displayed
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(me.ShouldDisplay(p, r));
                Message m = me.GetMessageToDisplay(p, r);
                Assert.IsNotNull(m);
            }
        }

        [TestMethod]
        public void TestGetMessageToDisplay1()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            MessageEntry me = CreateSample1();
            Message m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);
            Assert.IsTrue("message" == m.Description[MessageDescription.KeyMessage]);
            Assert.IsNotNull(m.Description.Title);
            Assert.IsNotNull(m.Description.ImageUrl);
        }

        [TestMethod]
        private static MessageEntry CreateSample2()
        {
            return CreateMessageEntry("{" + "	\"descriptions\":{" + "		\"en\":{"
                                      + "			\"message\":\"en - message\","
                                      + "			\"title\":\"en - title\","
                                      + "			\"imageUrl\":\"en - imageUrl\"" + "		}," + "		\"*\":{"
                                      + "			\"message\":\"any - message\","
                                      + "			\"title\":\"any - title\","
                                      + "			\"imageUrl\":\"any - imageUrl\"" + "		}" + "	},"
                                      + "	\"maxDisplayCount\":3," + "	\"id\":557" + "}");
        }

        [TestMethod]
        public void TestShouldDisplay2()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            Message m;

            MessageEntry me = CreateSample2();
            Assert.IsNotNull(me);

            Assert.IsTrue(me.Id == 557);

            // always should be displayed 3 times

            // 1
            Assert.IsTrue(me.ShouldDisplay(p, r));
            m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);

            // 2
            Assert.IsTrue(me.ShouldDisplay(p, r));
            m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);

            // 3
            Assert.IsTrue(me.ShouldDisplay(p, r));
            m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);

            // 4 - should not be displayed
            Assert.IsFalse(me.ShouldDisplay(p, r));
            m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);

            // 5 - should not be displayed
            Assert.IsFalse(me.ShouldDisplay(p, r));
            m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void TestGetMessageToDisplay2()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            MessageEntry me = CreateSample2();
            Message m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);
            Assert.AreEqual("en - message", m.Description.Body);
            Assert.AreEqual("en - title", m.Description.Title);
            Assert.AreEqual("en - imageUrl", m.Description.ImageUrl);
        }

        private static MessageEntry CreateSample3()
        {
            return CreateMessageEntry("{" + "	\"descriptions\":{" + "		\"en\":{"
                                      + "			\"message\":\"en - message\","
                                      + "			\"title\":\"en - title\","
                                      + "			\"imageUrl\":\"en - imageUrl\"" + "		}," + "		\"*\":{"
                                      + "			\"message\":\"any - message\","
                                      + "			\"title\":\"any - title\","
                                      + "			\"imageUrl\":\"any - imageUrl\"" + "		}" + "	},"
                                      + "	\"displayAfterDate\":\"2012-01-01\","
                                      + "	\"displayBeforeDate\":\"2013-01-01 01:01\","
                                      + "	\"targetWebsiteUrl\":\"http://example.com/\"" + "}");
        }
        [TestMethod]
        public void TestShouldDisplay3()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            DateTime d;

            MessageEntry me = CreateSample3();
            Assert.IsNotNull(me);

            //Calendar calendar = Calendar.getInstance(Locale.ENGLISH);

            // should not be displayed on 2011-12-31
            d = new DateTime(2011, 12, 31, 0, 0, 0);
            //calendar.set(2011, Calendar.DECEMBER, 31, 0, 0, 0);
            //d = calendar.getTime();
            Assert.IsFalse(me.ShouldDisplay(p, r, d));

            // should not be displayed on 2011-12-31 23:59
            //calendar.set(2011, Calendar.DECEMBER, 31, 23, 59, 0);
            d = new DateTime(2011, 12, 31, 23, 59, 0);
            //d = calendar.getTime();
            Assert.IsFalse(me.ShouldDisplay(p, r, d));

            // should be displayed on 2012-01-01 00:01
            //calendar.set(2012, Calendar.JANUARY, 1, 0, 1, 0);
            d = new DateTime(2012, 1, 1, 0, 1, 0);
            //d = calendar.getTime();
            Assert.IsTrue(me.ShouldDisplay(p, r, d));

            // should be displayed on 2012-06-06 12:30
            //calendar.set(2012, Calendar.JUNE, 6, 12, 30, 0);
            d = new DateTime(2012, 6, 6, 12, 30, 0);
            //d = calendar.getTime();
            Assert.IsTrue(me.ShouldDisplay(p, r, d));

            // should be displayed on 2012-12-31 23:59
            //calendar.set(2012, Calendar.DECEMBER, 31, 23, 59);
            d = new DateTime(2012, 12, 31, 23, 59, 0);
            //d = calendar.getTime();
            Assert.IsTrue(me.ShouldDisplay(p, r, d));

            // should be displayed on 2013-01-01 01:00
            //calendar.set(2013, Calendar.JANUARY, 1, 1, 0, 0);
            d = new DateTime(2013, 1, 1, 1, 0, 0);
            //d = calendar.getTime();
            Assert.IsTrue(me.ShouldDisplay(p, r, d));

            // should not be displayed on 2013-01-01 01:01
            //calendar.set(2013, Calendar.JANUARY, 1, 1, 1, 0);
            d = new DateTime(2013, 1, 1, 1, 1, 0);
            //d = calendar.getTime();
            Assert.IsFalse(me.ShouldDisplay(p, r, d));

            // should not be displayed on 2013-06-06 12:30
            //calendar.set(2013, Calendar.JUNE, 5, 12, 30, 0);
            d = new DateTime(2013, 6, 5, 12, 30, 0);
            //d = calendar.getTime();
            Assert.IsFalse(me.ShouldDisplay(p, r, d));
        }
        [TestMethod]
        public void TestGetMessageToDisplay3()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            MessageEntry me = CreateSample3();
            Message m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);
            Assert.AreEqual("http://example.com/", m.TargetWebsiteUrl.AbsoluteUri);
            Assert.AreEqual("en - message", m.Description.Body);
            Assert.AreEqual("en - title", m.Description.Title);
            Assert.AreEqual("en - imageUrl", m.Description.ImageUrl);
        }

        private static MessageEntry CreateSample4()
        {
            return CreateMessageEntry("{"
                                      + "	\"filters\":{"
                                      + "		\"appVersionCode\":\"1.0.0.0\""
                                      + "	},"
                                      + "	\"descriptions\":{"
                                      + "		\"*\":{"
                                      + "			\"message\":\"any - message\","
                                      + "			\"title\":\"any - title\","
                                      + "			\"imageUrl\":\"any - imageUrl\""
                                      + "		}"
                                      + "	},"
                                      + "	\"targetGooglePlay\":\"true\","
                                      + "	\"targetPackageName\":\"com.sample.app1\""
                                      + "}");
        }
        [TestMethod]
        public void TestShouldDisplay4()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();

            MessageEntry me = CreateSample4();
            Assert.IsNotNull(me);

            // check if filters are working properly

            Assert.IsTrue(me.ShouldDisplay(p, r));

            p[Properties.KeyAppVersion] = "4";
            Assert.IsFalse(me.ShouldDisplay(p, r));
        }
        [TestMethod]
        public void TestGetMessageToDisplay4()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            MessageEntry me = CreateSample4();
            Message m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);
            //Assert.AreEqual(true, m.TargetMarketplace);
            Assert.AreEqual("com.sample.app1", m.TargetPackageId);
            Assert.AreEqual("any - message", m.Description.Body);
            Assert.AreEqual("any - title", m.Description.Title);
            Assert.AreEqual("any - imageUrl", m.Description.ImageUrl);
        }

        private static MessageEntry CreateSample5()
        {
            return CreateMessageEntry("{"
                                      + "	\"descriptions\":{"
                                      + "		\"*\":{"
                                      + "			\"message\":\"any - message\","
                                      + "			\"title\":\"any - title\","
                                      + "			\"imageUrl\":\"any - imageUrl\""
                                      + "		}"
                                      + "	},"
                                      + "	\"targetGooglePlay\":\"true\","
                                      + "	\"targetPackageName\":\"com.sample.app1\","
                                      + "	\"displayPeriodInHours\":5"
                                      + "}");
        }
        [TestMethod]
        public void TestShouldDisplay5()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            Message m;
            DateTime d;

            MessageEntry me = CreateSample5();
            Assert.IsNotNull(me);

            //Calendar calendar = Calendar.getInstance(Locale.ENGLISH);

            // should be displayed when it is called first time
            d = DateTime.Now;
            Assert.IsTrue(me.ShouldDisplay(p, r, d));

            for (int i = 0; i < 10; i++)
            {
                m = me.GetMessageToDisplay(p, r, d);
                Assert.IsNotNull(m);
                // id should be auto generated
                Assert.IsTrue(me.Id != 0);

                // should NOT be displayed when it is called second time immediately
                Assert.IsFalse(me.ShouldDisplay(p, r, d));

                // should NOT be displayed when it is called second 3 hours later
                d = d.AddHours(3);
                Assert.IsFalse(me.ShouldDisplay(p, r, d));

                // should NOT be displayed when it is called second 4 hours later
                d = d.AddHours(1);
                Assert.IsFalse(me.ShouldDisplay(p, r, d));

                // should NOT be displayed when it is called second 30 minutes later
                d = d.AddMinutes(30);
                Assert.IsFalse(me.ShouldDisplay(p, r, d));

                // should be displayed when it is called second 30 minutes later
                d = d.AddMinutes(30);
                Assert.IsTrue(me.ShouldDisplay(p, r, d));

                // should be displayed when it is called second 3 hours later
                d = d.AddHours(3);
                Assert.IsTrue(me.ShouldDisplay(p, r, d));
            }
        }
        [TestMethod]
        public void TestGetMessageToDisplay5()
        {
            Properties p = createProperties();
            MessageDisplayRecords r = createRecords();
            MessageEntry me = CreateSample5();
            Message m = me.GetMessageToDisplay(p, r);
            Assert.IsNotNull(m);
            //Assert.AreEqual(true, m.TargetMarketplace);
            Assert.AreEqual("com.sample.app1", m.TargetPackageId);
            Assert.AreEqual("any - message", m.Description.Body);
            Assert.AreEqual("any - title", m.Description.Title);
            Assert.AreEqual("any - imageUrl", m.Description.ImageUrl);
        }
    }
}