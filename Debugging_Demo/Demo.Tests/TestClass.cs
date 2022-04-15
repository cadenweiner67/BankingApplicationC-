// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using Debugging_Demo;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;


namespace DemoTests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestRemoveCharIndex()
        {
            DebugExample debug = new DebugExample();
            Assert.That(debug.RemoveCharIndex("Hello World", 9), Is.EqualTo("Hello Word"));
        }
        [Test]
        public void TestCountingSheep()
        {
            DebugExample debug = new DebugExample();
            string[] sheepNames = new string[] { "Norman", "Cooper", "Ferdinand", "Roger", "Norman", "Dougal", "Diablo", "Shrek", "Oliver", "Tom", "Linus", "Dougal", "Huck", "Owen", "Owen", "Russel", "Luke", "Raymond", "Tom", "Lars", "Norman" };
            Assert.That(debug.CountingSheep(sheepNames), Is.EqualTo("Norman"));
        }
        [Test]
        public void TestMathOperations()
        {
            DebugExample debug = new DebugExample();
            Assert.AreEqual(debug.MathOperations().GetType(), typeof(int));

        }
        [Test]
        public void TestConvert()
        {
            DebugExample debug = new DebugExample();
            Assert.AreEqual(debug.ConvertFromInchesToMeters(60), 1.524);
        }
    }
}
