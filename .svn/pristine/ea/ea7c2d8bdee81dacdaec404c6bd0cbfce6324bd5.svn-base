using System.ComponentModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Turkcell.Updater.UnitTests
{
    [TestClass]
    public class FilterTest
    {
        
        [TestMethod]
        [Description("")]
        public void TestIsMatchesWith1()
        {
            var filter = new Filter(null, null);
            
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("x"));
            Assert.IsTrue(filter.IsMatchesWith("x, y"));

            filter = new Filter(null, "");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("x"));
            Assert.IsTrue(filter.IsMatchesWith("x, y"));

            filter = new Filter(null, "*");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("x"));
            Assert.IsTrue(filter.IsMatchesWith("x, y"));

            filter = new Filter(null, "   ");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("x"));
            Assert.IsTrue(filter.IsMatchesWith("x, y"));

            filter = new Filter(null, " * ");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("x"));
            Assert.IsTrue(filter.IsMatchesWith("x, y"));

            filter = new Filter(null, null);
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(" "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith(" x ,  y "));

            filter = new Filter(null, "");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith("   "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith(" x , y "));

            filter = new Filter(null, "*");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith("   "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith(" x , y "));

            filter = new Filter(null, "a,*");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith("   "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith(" x , y "));

            filter = new Filter(null, "a");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));

            filter = new Filter(null, "ab");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("b"));
            Assert.IsTrue(filter.IsMatchesWith(" ab "));
            Assert.IsTrue(filter.IsMatchesWith("A b"));
            Assert.IsTrue(filter.IsMatchesWith(" A b"));
            Assert.IsTrue(filter.IsMatchesWith(" A b "));
            Assert.IsFalse(filter.IsMatchesWith(" abc "));
            Assert.IsFalse(filter.IsMatchesWith(" aab "));


            filter = new Filter(null, "a,");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));

            filter = new Filter(null, " a ,");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));

            filter = new Filter(null, "A,");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));

            filter = new Filter(null, " A ,");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));

            filter = new Filter(null, "a,b");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));
            Assert.IsTrue(filter.IsMatchesWith("b"));
            Assert.IsTrue(filter.IsMatchesWith(" b "));
            Assert.IsTrue(filter.IsMatchesWith("B"));
            Assert.IsTrue(filter.IsMatchesWith(" B "));

            filter = new Filter(null, " A , B ");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));
            Assert.IsTrue(filter.IsMatchesWith("b"));
            Assert.IsTrue(filter.IsMatchesWith(" b "));
            Assert.IsTrue(filter.IsMatchesWith("B"));
            Assert.IsTrue(filter.IsMatchesWith(" B "));


            filter = new Filter(null, " A* , B ");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" a "));
            Assert.IsTrue(filter.IsMatchesWith("A"));
            Assert.IsTrue(filter.IsMatchesWith(" A "));
            Assert.IsTrue(filter.IsMatchesWith("b"));
            Assert.IsTrue(filter.IsMatchesWith(" b "));
            Assert.IsTrue(filter.IsMatchesWith("B"));
            Assert.IsTrue(filter.IsMatchesWith(" B "));
            Assert.IsTrue(filter.IsMatchesWith("abc"));

            filter = new Filter(null, " Ab* ");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith(" ab "));
            Assert.IsFalse(filter.IsMatchesWith("cAb"));
            Assert.IsFalse(filter.IsMatchesWith("cAbC"));
            Assert.IsTrue(filter.IsMatchesWith(" aBC"));
            Assert.IsFalse(filter.IsMatchesWith(" cbA "));

            filter = new Filter(null, " Ab*d ");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("abcd"));
            Assert.IsTrue(filter.IsMatchesWith(" abcd "));
            Assert.IsTrue(filter.IsMatchesWith(" ABCD "));
            Assert.IsTrue(filter.IsMatchesWith(" ABCCCCCD "));
            Assert.IsTrue(filter.IsMatchesWith("abd"));
            Assert.IsFalse(filter.IsMatchesWith("aabd"));
            Assert.IsFalse(filter.IsMatchesWith("aabeed"));
            Assert.IsFalse(filter.IsMatchesWith("adab"));

            filter = new Filter(null, " *aB ");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("b"));
            Assert.IsTrue(filter.IsMatchesWith(" cdab "));
            Assert.IsTrue(filter.IsMatchesWith(" ABCDab"));
            Assert.IsTrue(filter.IsMatchesWith("ABCCCCCDAB"));
            Assert.IsFalse(filter.IsMatchesWith("dba"));
            Assert.IsFalse(filter.IsMatchesWith("aabd"));
            Assert.IsTrue(filter.IsMatchesWith("xxxbeedab"));
            Assert.IsFalse(filter.IsMatchesWith("adba"));
            Assert.IsTrue(filter.IsMatchesWith("ab"));
            Assert.IsFalse(filter.IsMatchesWith("abcd"));
        }

        [TestMethod]
        public void TestIsMatchesWith2()
        {
            var filter = new Filter(null, " > 5");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" 6 "));
            Assert.IsTrue(filter.IsMatchesWith(" 1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 5 "));
            Assert.IsFalse(filter.IsMatchesWith(" -6 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));

            filter = new Filter(null, " >= 5");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("5"));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" 6 "));
            Assert.IsTrue(filter.IsMatchesWith(" 1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 4 "));
            Assert.IsFalse(filter.IsMatchesWith(" -6 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));

            filter = new Filter(null, "<56");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" 6 "));
            Assert.IsFalse(filter.IsMatchesWith(" 1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 56 "));
            Assert.IsTrue(filter.IsMatchesWith(" 55 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));

            filter = new Filter(null, "< -56");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" -60 "));
            Assert.IsFalse(filter.IsMatchesWith(" 1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" -1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 56 "));
            Assert.IsTrue(filter.IsMatchesWith(" -57 "));
            Assert.IsFalse(filter.IsMatchesWith(" -56 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));

            filter = new Filter(null, "<= -57");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" -60 "));
            Assert.IsFalse(filter.IsMatchesWith(" 1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" -1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 57 "));
            Assert.IsTrue(filter.IsMatchesWith(" -57 "));
            Assert.IsFalse(filter.IsMatchesWith(" -56 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));

            filter = new Filter(null, "<> -57");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" -60 "));
            Assert.IsTrue(filter.IsMatchesWith(" 1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" -1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" 57 "));
            Assert.IsFalse(filter.IsMatchesWith(" -57 "));
            Assert.IsTrue(filter.IsMatchesWith(" -56 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));
        }
        
        [TestMethod]
        public void TestIsMatchesWith3()
        {
            var filter = new Filter(null, "''");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("6"));
            Assert.IsFalse(filter.IsMatchesWith(" 6 "));
            Assert.IsFalse(filter.IsMatchesWith(" 1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 5 "));
            Assert.IsFalse(filter.IsMatchesWith(" -6 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));
        }

        [TestMethod]
        public void TestIsMatchesWith4()
        {
            var filter = new Filter(null, "!''");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith(""));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsTrue(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" 6 "));
            Assert.IsTrue(filter.IsMatchesWith(" 1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" 5 "));
            Assert.IsTrue(filter.IsMatchesWith(" -6 "));
            Assert.IsTrue(filter.IsMatchesWith("6x"));
        }

        [TestMethod]
        public void TestIsMatchesWith5()
        {
            var filter = new Filter(null, "!*");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith(""));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("6"));
            Assert.IsFalse(filter.IsMatchesWith(" 6 "));
            Assert.IsFalse(filter.IsMatchesWith(" 1903271 "));
            Assert.IsFalse(filter.IsMatchesWith(" 5 "));
            Assert.IsFalse(filter.IsMatchesWith(" -6 "));
            Assert.IsFalse(filter.IsMatchesWith("6x"));
        }

        [TestMethod]
        public void TestIsMatchesWith6()
        {
            var filter = new Filter(null, "!a");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("   "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("A"));
            Assert.IsFalse(filter.IsMatchesWith(" a "));
            Assert.IsFalse(filter.IsMatchesWith(" A "));
            Assert.IsTrue(filter.IsMatchesWith(" AA "));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" 6 "));
            Assert.IsTrue(filter.IsMatchesWith(" 1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" 5 "));
            Assert.IsTrue(filter.IsMatchesWith(" -6 "));
            Assert.IsTrue(filter.IsMatchesWith("6x"));
        }

        [TestMethod]
        public void TestIsMatchesWith7()
        {
            var filter = new Filter(null, "!a,*");
            Assert.IsTrue(filter.IsMatchesWith(null));
            Assert.IsTrue(filter.IsMatchesWith(""));
            Assert.IsTrue(filter.IsMatchesWith("   "));
            Assert.IsTrue(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsFalse(filter.IsMatchesWith("A"));
            Assert.IsFalse(filter.IsMatchesWith(" a "));
            Assert.IsFalse(filter.IsMatchesWith(" A "));
            Assert.IsTrue(filter.IsMatchesWith(" AA "));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith(" 6 "));
            Assert.IsTrue(filter.IsMatchesWith(" 1903271 "));
            Assert.IsTrue(filter.IsMatchesWith(" 5 "));
            Assert.IsTrue(filter.IsMatchesWith(" -6 "));
            Assert.IsTrue(filter.IsMatchesWith("6x"));
        }

        [TestMethod]
        public void TestIsMatchesWith8()
        {
            var filter = new Filter(null, "!a*cd, Ab*d ");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("abed"));
            Assert.IsTrue(filter.IsMatchesWith(" abed "));
            Assert.IsTrue(filter.IsMatchesWith(" ABeD "));
            Assert.IsTrue(filter.IsMatchesWith(" ABCCCCED "));
            Assert.IsFalse(filter.IsMatchesWith("abcd"));
            Assert.IsFalse(filter.IsMatchesWith(" abcd "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCCD "));
            Assert.IsTrue(filter.IsMatchesWith("abd"));
            Assert.IsFalse(filter.IsMatchesWith("aabd"));
            Assert.IsFalse(filter.IsMatchesWith("aabeed"));
            Assert.IsFalse(filter.IsMatchesWith("adab"));
        }

        [TestMethod]
        public void TestIsMatchesWith9()
        {
            var filter = new Filter(null, "! *cc*, ! a*cd, Ab*d");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("abed"));
            Assert.IsTrue(filter.IsMatchesWith(" abed "));
            Assert.IsTrue(filter.IsMatchesWith(" ABeD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCED "));
            Assert.IsTrue(filter.IsMatchesWith(" ABCBCBCED "));
            Assert.IsFalse(filter.IsMatchesWith("abcd"));
            Assert.IsFalse(filter.IsMatchesWith(" abcd "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCCD "));
            Assert.IsTrue(filter.IsMatchesWith("abd"));
            Assert.IsFalse(filter.IsMatchesWith("aabd"));
            Assert.IsFalse(filter.IsMatchesWith("aabeed"));
            Assert.IsFalse(filter.IsMatchesWith("adab"));
        }

        [TestMethod]
        public void TestIsMatchesWith10()
        {
            var filter = new Filter(null, "Ab*d, ! a*cd, ! *cc*");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("abed"));
            Assert.IsTrue(filter.IsMatchesWith(" abed "));
            Assert.IsTrue(filter.IsMatchesWith(" ABeD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCED "));
            Assert.IsTrue(filter.IsMatchesWith(" ABCBCBCED "));
            Assert.IsFalse(filter.IsMatchesWith("abcd"));
            Assert.IsFalse(filter.IsMatchesWith(" abcd "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCCD "));
            Assert.IsTrue(filter.IsMatchesWith("abd"));
            Assert.IsFalse(filter.IsMatchesWith("aabd"));
            Assert.IsFalse(filter.IsMatchesWith("aabeed"));
            Assert.IsFalse(filter.IsMatchesWith("adab"));
        }

        [TestMethod]
        public void TestIsMatchesWith11()
        {
            var filter = new Filter(null, ">5, !>=10, <3, a*d, !*c*, 4");
            Assert.IsFalse(filter.IsMatchesWith(null));
            Assert.IsFalse(filter.IsMatchesWith("   "));
            Assert.IsFalse(filter.IsMatchesWith(" x "));
            Assert.IsFalse(filter.IsMatchesWith("a"));
            Assert.IsTrue(filter.IsMatchesWith("abed"));
            Assert.IsTrue(filter.IsMatchesWith(" abed "));
            Assert.IsTrue(filter.IsMatchesWith(" ABeD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCED "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCBCBCED "));
            Assert.IsFalse(filter.IsMatchesWith("abcd"));
            Assert.IsFalse(filter.IsMatchesWith(" abcd "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCD "));
            Assert.IsFalse(filter.IsMatchesWith(" ABCCCCCD "));
            Assert.IsTrue(filter.IsMatchesWith("abd"));
            Assert.IsTrue(filter.IsMatchesWith("aabd"));
            Assert.IsTrue(filter.IsMatchesWith("aabeed"));

            Assert.IsTrue(filter.IsMatchesWith("-1"));
            Assert.IsTrue(filter.IsMatchesWith("0"));
            Assert.IsTrue(filter.IsMatchesWith("1"));
            Assert.IsTrue(filter.IsMatchesWith("2"));
            Assert.IsFalse(filter.IsMatchesWith("3"));
            Assert.IsTrue(filter.IsMatchesWith("4"));
            Assert.IsFalse(filter.IsMatchesWith("5"));
            Assert.IsTrue(filter.IsMatchesWith("6"));
            Assert.IsTrue(filter.IsMatchesWith("7"));
            Assert.IsTrue(filter.IsMatchesWith("8"));
            Assert.IsTrue(filter.IsMatchesWith("9"));
            Assert.IsFalse(filter.IsMatchesWith("10"));
            Assert.IsFalse(filter.IsMatchesWith("11"));
        }

    }
}
