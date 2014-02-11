using System;
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

        [Test]
        public void TestExceptionsForNegatives()
        {
            Exception exception = Assert.Throws<Exception>(new TestDelegate(() => calculator.Add("1,-1,-2")));
            Assert.That(exception.Message, Is.EqualTo("negatives not allowed: -1 -2"));
        }

        [Test]
        public void TestExceptionsForOneNegativeNumber()
        {
            Exception exception = Assert.Throws<Exception>(new TestDelegate(() => calculator.Add("-1")));
            Assert.That(exception.Message, Is.EqualTo("negatives not allowed: -1"));
        }

    }
}
