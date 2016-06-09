using System;

namespace transpiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string SQL = @"SELECT max(arg), field, min(lofw_s5t) from table group by field where field = 2 + 5";
            SQL = @"SELECT max(reputation), id, name FROM users WHERE id = 2 + 5";

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
            catch (Exception err)
            {
                Helpers.Logger(err);
            }

            Console.Read();
        }
    }
}
