using System;

namespace processor
{
    class Program
    {
        static void Main(string[] args)
        {
            string SQL = @"SELECT max(arg), field, min(lofw_s5t) from table group by field where field = 2 + 5";
            var tokens = new Lexer(SQL).Analyze();

            foreach (var token in tokens.GetList())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[{0,2}]", token.Index);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0,10}", token.Value);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" => ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(token.Type);
                Console.WriteLine();
            }

            Console.WriteLine();
            try
            {
                Parser.Parse(tokens);
            } catch(ArgumentException err)
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
