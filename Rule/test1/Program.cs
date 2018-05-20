using System;
using RuleFactory.RulesFactory;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var rule = ConstantRulesFactory.CreateConstantRule<string>("value");

            var compile = rule.Compile();

            var result = rule.Get();

            //var foo = GetSomeNullValue();
        }

        private static string GetSomeNullValue()
        {
            throw new NotImplementedException();
            //return ((int?) null) ?? null;
        }
    }
}
