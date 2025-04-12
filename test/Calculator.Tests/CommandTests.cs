using System;
using System.Collections.Generic;
using System.Globalization;
using Calculations.Models;
using Calculations.Models.Ast;
using Calculations.Models.Commands;

namespace Calculator.Tests
{
    [TestFixture]
    public class CommandTests
    {
        [TestCase("1 + 2", "3")]
        [TestCase("(1 + 2)", "3")]
        [TestCase("(1 + 2)*6 - (14 - 4) / 2 - 0.5", "12.5")]
        [TestCase("-(1 + 2)", "-3")]
        [TestCase("-1", "-1")]
        public void CalculateTest(string text, string expected)
        {
            Assert.True(decimal.TryParse(expected, NumberStyles.Float, CultureInfo.InvariantCulture, out var r));

            var tokens = text.GetCalculatorTokens();
            var ast = AstBuilder.BuildAst(tokens);
            var cmd = CmdBuilder.Build(ast);

            var result = cmd.Calculate(EmptyVariableValueProvider.Instance);

            Console.WriteLine($"Result: {result}");

            Assert.That(result, Is.EqualTo(r));
        }

        [Test]
        public void CalculateWithVarsTest1()
        {
            var tokens = "a + b/(2 + 7) - a*b".GetCalculatorTokens();
            var ast = AstBuilder.BuildAst(tokens);
            var cmd = CmdBuilder.Build(ast);

            var variables = new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            }.AsVariableValueProvider();

            var result = cmd.Calculate(variables);

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-66m));
        }
    }
}
