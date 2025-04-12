using System;
using Calculations.Models;
using Calculations.Models.Ast;

namespace Calculator.Tests
{
    [TestFixture]
    public class AstTest
    {
        [TestCase("1 + 2", "(1 + 2)")]
        [TestCase("(1 + 2)", "(1 + 2)")]
        [TestCase("(1 + 2)*6 - (14 - 4) / 2 - 0.5", "((((1 + 2) * 6) - ((14 - 4) / 2)) - 0.5)")]
        [TestCase("-(1 + 2)", "-(1 + 2)")]
        [TestCase("-(((1 + 2)))", "-(1 + 2)")]
        [TestCase("-1", "-1")]
        public void GetAstTest(string text, string result)
        {
            var tokens = text.GetCalculatorTokens();
            foreach (var ti in tokens)
            {
                Console.WriteLine($"{ti.Token}: '{ti.Value}'");
            }

            var ast = AstBuilder.BuildAst(tokens);

            var expression = ast.ToString();
            Console.WriteLine(expression);

            Assert.That(result, Is.EqualTo(expression));
        }
    }
}
