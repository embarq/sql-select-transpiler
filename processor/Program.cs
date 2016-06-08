using System;

namespace processor
{
    class Program
    {
        static void Main(string[] args)
        {
            string SQL = @"SELECT max(arg), field, min(lofw_s5t) from table group by field where field = 2 + 5";
            SQL = @"SELECT max(arg), field from another_table where field = 2 + 5";
            var tokens = new Lexer(SQL).Analyze();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(SQL);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(tokens.TokenString);
            Console.WriteLine();

            try
            {
                Parser parser = new Parser(tokens);
            }
            catch (ArgumentException err)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(err);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.Read();
        }
    }
}
