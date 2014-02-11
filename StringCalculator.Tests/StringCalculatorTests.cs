using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private StringCalculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new StringCalculator();
        }

        [Test]
        public void TestAddEmptyString()
        {
            Assert.That(calculator.Add(""), Is.EqualTo(0));
        }

        [Test]
        public void TestAddOneNumber()
        {
            Assert.That(calculator.Add("1"), Is.EqualTo(1));
        }
    }
}
