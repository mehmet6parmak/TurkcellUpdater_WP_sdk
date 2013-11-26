using System;
using System.Collections.Generic;
using LitJson;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Turkcell.Updater.UnitTests
{
    [TestClass]
    public class UpdateEntryTests
    {
        private static UpdateEntry CreateVersionEntry(String json)
        {
            return new UpdateEntry(JsonMapper.ToObject(json));
        }

        [TestMethod]
        public void TestIsMatches()
        {
            UpdateEntry e = getExampleFullUpdateEntry();
            Properties p = getExampleFullProperties();

            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyAppVersion] = "4.0";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyAppVersion] = "5.0";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyAppVersion] = "7.0";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyAppVersion] = "11.0";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = "fr";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = "tr";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = "tw";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyCustomPrefix + "costum-key-2"] = "costum-key-2xx";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyCustomPrefix + "costum-key-3"] = "costum-third-key";
            Assert.IsTrue(e.IsMatches(p));

            /* Should not match */

            p = getExampleFullProperties();
            p[Properties.KeyAppVersion] = "";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyAppVersion] = null;
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyAppVersion] = "6.0";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyAppVersion] = " 8.0 ";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyDeviceLanguage] = "";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyDeviceLanguage] = null;
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyDeviceLanguage] = "ch";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyDeviceLanguage] = "atr";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyDeviceLanguage] = "een";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyDeviceLanguage] = "eng";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-1"] = "";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-1"] = null;
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-1"] = "costum-key-1x";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-1"] = "x-costum-key-1";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-1"] = "x-costum-key";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-2"] = "costum-key-";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-3"] = "costum-key";
            Assert.IsFalse(e.IsMatches(p));

            p = getExampleFullProperties();
            p[Properties.KeyCustomPrefix + "costum-key-3"] = "costum-super-key-2";
            Assert.IsFalse(e.IsMatches(p));

            /* Any thing should match */
            e = getExampleMinimalUpdateEntry();
            p = getExampleFullProperties();
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyCustomPrefix + "costum-key-1"] = "";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyCustomPrefix + "costum-key-1"] = null;
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyCustomPrefix + "costum-key-1"] = "costum-key-1x";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyCustomPrefix + "costum-key-2"] = "costum-key-";
            Assert.IsTrue(e.IsMatches(p));

            p = getExampleFullProperties();

            p[Properties.KeyAppVersion] = "";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyAppVersion] = " 8.0 ";
            Assert.IsTrue(e.IsMatches(p));
           
            p[Properties.KeyDeviceLanguage] = "";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = null;
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = "ch";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = "atr";
            Assert.IsTrue(e.IsMatches(p));

            p[Properties.KeyDeviceLanguage] = "een";
            Assert.IsTrue(e.IsMatches(p));

            p = getExampleEmptyProperties();
            Assert.IsTrue(e.IsMatches(p));
        }

        private Properties getExampleFullProperties()
        {
            var pm = new Dictionary<String, String>
                {
                    {Properties.KeyAppVersion, "2.2.3"},
                    {Properties.KeyDeviceIsTablet, "true"},
                    {Properties.KeyDeviceLanguage, "en"},
                    {Properties.KeyCustomPrefix + "costum-key-1", "costum-key-1"},
                    {Properties.KeyCustomPrefix + "costum-key-2", "costum-key-2"},
                    {Properties.KeyCustomPrefix + "costum-key-3", "costum-super-key"}
                };

            var task = Properties.CreateInstance(pm);
            task.Wait();
            return task.Result;
        }

        private Properties getExampleEmptyProperties()
        {
            var pm = new Dictionary<String, String>();
            var task = Properties.CreateInstance(pm);
            task.Wait();
            return task.Result;
        }

        private UpdateEntry getExampleFullUpdateEntry()
        {
            return CreateVersionEntry("{\r\n"
                    + "    \"filters\": {\n"
                    //+ "        \"appVersionName\": \"2.2.3, 3.1.*, 4.*b*\",\n"
                    + "        \"appVersionCode\": \"<= 5.0, 7.0, > 10.0\",\n"
                    + "        \"deviceIsTablet\": \"false\",\n"
                    + "        \"deviceLanguage\": \"en, fr, t*\",\n"
                    + "        \"x-costum-key-1\": \"costum-key-1\",\n"
                    + "        \"x-costum-key-2\": \"costum-key-2*\",\n"
                    + "        \"x-costum-key-3\": \"costum-*-key\"\n"
                    + "    },\n"
                    + "    \"descriptions\": {\n"
                    + "        \"tr\": {\n"
                    + "            \"message\": \"tr - message\",\n"
                    + "            \"whatIsNew\": \"tr - what is new\",\n"
                    + "            \"warnings\": \"tr - warnings\"\n"
                    + "        },\n"
                    + "        \"en\": {\n"
                    + "            \"message\": \"en - message\",\n"
                    + "            \"whatIsNew\": \"en - what is new\",\n"
                    + "            \"warnings\": \"en - warnings\"\n"
                    + "        },\n"
                    + "        \"*\": {\n"
                    + "            \"message\": \"any - message\",\n"
                    + "            \"whatIsNew\": \"any - what is new\",\n"
                    + "            \"warnings\": \"any - warnings\"\n"
                    + "        }\n"
                    + "    },\n"
                    + "    \"targetVersionCode\": \"6.3\",\n"
                    + "    \"targetPackageName\": \"com.sample.app1\",\n"
                    + "    \"targetUriSchema\": \"http://sample.com/index.html\",\n"
                    + "    \"forceUpdate\": \"true\",\n"
                    + "    \"forceUninstall\": \"false\"\n" + "}");

        }

        private UpdateEntry getExampleMinimalUpdateEntry()
        {
            return CreateVersionEntry("{\n" + "\n"
                    + "	\"descriptions\": \"any - message\",\n"
                    + "	\"targetVersionCode\": \"23\",\n"
                    + "	\"targetUriSchema\": \"http://sample.com/index.html\",\n"
                    + "	\"forceUpdate\": \"true\",\n"
                    + "	\"forceUninstall\": \"false\"\n" + "}");
        }

        [TestMethod]
        public void TestGetVersion()
        {
            Properties p = getExampleFullProperties();
            Update update = getExampleFullUpdateEntry().GetUpdate(p);

            Assert.IsNotNull(update);
            Assert.AreEqual(update.ForceExit, false);
            Assert.AreEqual(update.ForceUpdate, true);
            Assert.AreEqual(update.TargetAppUriSchema, new Uri("http://sample.com/index.html"));
            Assert.AreEqual(update.TargetPackageId, "com.sample.app1");
            Assert.IsNotNull(update.Description);

            Assert.AreEqual(update.Description.LanguageCode, "en");
            Assert.AreEqual(update.Description.Message, "en - message");
            Assert.AreEqual(update.Description.WhatIsNew, "en - what is new");
            Assert.AreEqual(update.Description.Warnings, "en - warnings");

            p[Properties.KeyDeviceLanguage] = "tr";
            update = getExampleFullUpdateEntry().GetUpdate(p);
            Assert.IsNotNull(update);
            Assert.AreEqual(update.ForceExit, false);
            Assert.AreEqual(update.ForceUpdate, true);
            Assert.AreEqual(update.TargetAppUriSchema, new Uri("http://sample.com/index.html"));
            Assert.AreEqual(update.TargetPackageId, "com.sample.app1");
            Assert.IsNotNull(update.Description);

            Assert.AreEqual(update.Description.LanguageCode, "tr");
            Assert.AreEqual(update.Description.Message, "tr - message");
            Assert.AreEqual(update.Description.WhatIsNew, "tr - what is new");
            Assert.AreEqual(update.Description.Warnings, "tr - warnings");

            p[Properties.KeyDeviceLanguage] = "tw";
            update = getExampleFullUpdateEntry().GetUpdate(p);
            Assert.IsNotNull(update);
            Assert.AreEqual(update.ForceExit, false);
            Assert.AreEqual(update.ForceUpdate, true);
            Assert.AreEqual(update.TargetAppUriSchema, new Uri("http://sample.com/index.html"));
            Assert.AreEqual(update.TargetPackageId, "com.sample.app1");
            Assert.IsNotNull(update.Description);

            Assert.AreEqual(update.Description.LanguageCode, null);
            Assert.AreEqual(update.Description.Message, "any - message");
            Assert.AreEqual(update.Description.WhatIsNew, "any - what is new");
            Assert.AreEqual(update.Description.Warnings, "any - warnings");

            p[Properties.KeyDeviceLanguage] = "";
            update = getExampleFullUpdateEntry().GetUpdate(p);
            Assert.IsNotNull(update);
            Assert.AreEqual(update.ForceExit, false);
            Assert.AreEqual(update.ForceUpdate, true);
            Assert.AreEqual(update.TargetAppUriSchema, new Uri("http://sample.com/index.html"));
            Assert.AreEqual(update.TargetPackageId, "com.sample.app1");
            Assert.IsNotNull(update.Description);

            Assert.AreEqual(update.Description.LanguageCode, null);
            Assert.AreEqual(update.Description.Message, "any - message");
            Assert.AreEqual(update.Description.WhatIsNew, "any - what is new");
            Assert.AreEqual(update.Description.Warnings, "any - warnings");

            p[Properties.KeyDeviceLanguage] = null;
            update = getExampleFullUpdateEntry().GetUpdate(p);
            Assert.IsNotNull(update);
            Assert.AreEqual(update.ForceExit, false);
            Assert.AreEqual(update.ForceUpdate, true);
            Assert.AreEqual(update.TargetAppUriSchema, new Uri("http://sample.com/index.html"));
            Assert.AreEqual(update.TargetPackageId, "com.sample.app1");
            Assert.IsNotNull(update.Description);

            Assert.AreEqual(update.Description.LanguageCode, null);
            Assert.AreEqual(update.Description.Message, "any - message");
            Assert.AreEqual(update.Description.WhatIsNew, "any - what is new");
            Assert.AreEqual(update.Description.Warnings, "any - warnings");

            p[Properties.KeyAppPackageId] = null;
            update = getExampleFullUpdateEntry().GetUpdate(p);
            Assert.IsNotNull(update);
            Assert.AreEqual(update.ForceExit, false);
            Assert.AreEqual(update.ForceUpdate, true);
            Assert.AreEqual(update.TargetAppUriSchema, new Uri("http://sample.com/index.html"));
            Assert.AreEqual(update.TargetPackageId, "com.sample.app1");
            Assert.IsNotNull(update.Description);

            bool thrown = false;
            try
            {
                update = getExampleMinimalUpdateEntry().GetUpdate(p);
            }
            catch (UpdaterException)
            {
                thrown = true;
            }
            //Assert.IsTrue(thrown);

            Assert.IsTrue(String.IsNullOrEmpty(update.TargetPackageId));

            p[Properties.KeyAppPackageId] = "tr.com.turkcellteknoloji.turkcellupdater.test";
            update = getExampleMinimalUpdateEntry().GetUpdate(p);
            Assert.IsTrue(String.IsNullOrEmpty(update.TargetPackageId));
        }


    }
}
