using System;

namespace transpiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string SQL = @"SELECT id COUNT(name), age, MAX(reputation) FROM users GROUP BY name WHERE age > 18;";

            var tokens = new Lexer(SQL).Analyze();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(SQL);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            foreach(string tokenType in tokens.TokenList)
            {
                if (Config.Patterns.Statement.IsMatch(tokenType))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (Config.Patterns.Function.IsMatch(tokenType))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (tokenType.Equals("digit"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (tokenType.Equals("variable") || tokenType.Equals("variable"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("<{0}>", tokenType);
            }
            Console.WriteLine();

            try
            {
                Parser parser = new Parser(tokens);
            }
            catch (Exception err)
            {
                Helpers.Logger(err);
            }

            Console.Read();
        }
    }
}
