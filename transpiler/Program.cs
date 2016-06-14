using System;

namespace transpiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] queries =
            {
                @"SELECT id, COUNT(name), age, MAX(reputation) FROM users GROUP BY name WHERE age > 18;",
                @"SELECT man, FROM mans;",
                @"SELECT title, MAX(rate) films GROUP BY title;",
                @"SELECT id, name FROM users WHERE age > 18;"
            };
            
            foreach(string query in queries)
            {
                TokenCollection tokens = new Lexer(query).Analyze();
                bool parseResult = ParserTest(tokens);
                Console.WriteLine("Parser done with status: {0}", parseResult);
                Console.WriteLine();
            }

            Console.Read();
        }

        static bool ParserTest(TokenCollection tokens)
        {
            Parser parser = null;
            try
            {
                parser = new Parser(tokens);
            }
            catch (Exception err)
            {
                Helpers.Log(err);
                return false;
            }
            return parser.Status;
        }
    }
}
