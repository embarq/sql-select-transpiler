using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace processor
{
    class Parser
    {
        TokenCollection Tokens;

        public Parser(TokenCollection tokens)
        {
            this.Tokens = tokens;

            Console.WriteLine("Parser started...");

            if (!(tokens.HasType("select") && tokens.HasType("from")))
            {
                throw new ArgumentException("Missing `SELECT` or `FROM` statement", "tokens");
            }

            // Parsing starts from only statements
            foreach (Token token in tokens.List)
            {
                if (Config.Patterns.Statement.IsMatch(token.Type))
                {
                    ParseStatement(token);
                }
            }
        }

        TokenCollection GetStatementRange(Token statement)
        {
            
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = statement.Index + 1;
            Regex pattern = Config.Patterns.Statement.IsMatch(statement.Type) ?
                Config.Patterns.Statement : Config.Patterns.Function;

            try
            {
                while (!pattern.IsMatch(Tokens.List[tokenCounter].Type))
                {
                    tokenRange.Add(Tokens.Get(tokenCounter++));
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return tokenRange;
            }

            return tokenRange;
        }

        TokenCollection ParseStatement(Token statement) {
            TokenCollection range = GetStatementRange(statement);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("`{0}` range is {1}", 
                statement.Type,
                range.TokenString);

            range.Print();
            Console.ForegroundColor = ConsoleColor.White;

            return range;
        }
    }
}
