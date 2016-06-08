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

        TokenCollection ParseStatement(Token token)
        {
            TokenCollection range = GetStatementRange(token);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("`{0}` range is {1}",
                token.Type,
                range.TokenString);

            range.Print();
            Console.ForegroundColor = ConsoleColor.White;

            foreach (Token range_token in range.List)
            {

            }

            return range;
        }

        TokenCollection ParseFunction(Token token)
        {
            var range = GetFunctionRange(token);
            bool[] check = {
                Config.Patterns.Function.IsMatch(range.Get(0).Type),
                Config.Patterns.OpenParenthes.IsMatch(range.Get(1).Type),
                Config.Patterns.Variable.IsMatch(range.Get(2).Type),
                Config.Patterns.CloseParenthes.IsMatch(range.Get(3).Type)
            };

            if (check.All(item => item))
            {
                return range;
            }
            else
            {
                throw new Exception(string.Format(
                    "Invalid syntax in function {0}",
                    check.Select(item => item != true)));
            }
        }

        TokenCollection GetFunctionRange(Token token)
        {
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = token.Index + 1;

            try
            {
                while (Tokens.List[tokenCounter].Type != "separator")
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

        TokenCollection GetStatementRange(Token token)
        {
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = token.Index + 1;
            Regex pattern = Config.Patterns.Statement;

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
    }
}
