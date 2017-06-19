using DataAnalysisEngine;
using NUnit.Framework;

namespace DataAnalysisTest
{
    [TestFixture]
    public class DataOperationTest
    {
        [Test]
        public void TestIsInteger()
        {
            var isIntegerCase1 = DataOperations.IsInteger("2232212");
            Assert.AreEqual(isIntegerCase1, true);

            var isIntegerCase2 = DataOperations.IsInteger("-2232212");
            Assert.AreEqual(isIntegerCase2, true);

            var isIntegerCase3 = DataOperations.IsInteger("sdfsdfsf.2");
            Assert.AreEqual(isIntegerCase3, false);

            var isIntegerCase4 = DataOperations.IsInteger("-2ss221.2");
            Assert.AreEqual(isIntegerCase4, false);

            var isIntegerCase5 = DataOperations.IsInteger("sdfsdfsf");
            Assert.AreEqual(isIntegerCase5, false);

            var isIntegerCase6 = DataOperations.IsInteger("2234sw34");
            Assert.AreEqual(isIntegerCase6, false);
        }
    }
}
