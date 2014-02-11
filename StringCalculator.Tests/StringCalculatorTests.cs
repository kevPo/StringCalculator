using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void TestAddEmptyString()
        {
            var calculator = new StringCalculator();
            Assert.That(calculator.Add(""), Is.EqualTo(0));
        }
    }
}
