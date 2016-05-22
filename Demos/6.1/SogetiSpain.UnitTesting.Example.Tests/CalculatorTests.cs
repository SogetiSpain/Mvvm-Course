namespace SogetiSpain.UnitTesting.Example.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AddShouldReturnSumOfValues()
        {
            // Arrange -> "Disponer"
            var calculator = new Calculator();

            // Act -> "Actuar"
            int result = calculator.Add(6, 2);

            // Assert -> "Afirmar"
            Assert.AreEqual(8, result);
        }
    }
}