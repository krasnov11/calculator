using System;
using Calculations.Models;

namespace Calculator.Tests
{
    [TestFixture]
    public class AstCalculatorTest
    {
        [TestCase("1 + 2", "3")]
        [TestCase("(1 + 2)", "3")]
        [TestCase("(1 + 2)*6 - (14 - 4) / 2 - 0.5", "12.5")]
        [TestCase("-(1 + 2)", "-3")]
        [TestCase("-1", "-1")]
        public void GetAstTest(string text, string result)
        {
            var tokens = text.GetCalculatorTokens();
            foreach (var ti in tokens)
            {
                Console.WriteLine($"{ti.Token}: '{ti.Value}'");
            }
        }
    }
}
