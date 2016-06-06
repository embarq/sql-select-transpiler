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
                Console.Write("[{0,2}]", token.index);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0,10}", token.value);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" => ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(token.type);
                Console.WriteLine();
            }
            
            //Parser.Parse(TokenCollection);

            Console.Read();
        }
    }
}
