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

        [Test]
        public void TestMultipleNumbers()
        {
            Assert.That(calculator.Add("1,2"), Is.EqualTo(3));
        }

        [Test]
        public void TestNewLineDelimiter()
        {
            Assert.That(calculator.Add("1\n2,3"), Is.EqualTo(6));
        }

        [Test]
        public void TestDefinedDelimiter()
        {
            Assert.That(calculator.Add("//;\n1;2"), Is.EqualTo(3));
        }

    }
}
