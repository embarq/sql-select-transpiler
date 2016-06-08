using System.Collections.Generic;
using System.Linq;
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
                throw SqlException.StatementsException;
            }

            // Парсинг начинается с statement'ов
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
            TokenCollection range = null;
            try
            {
                range = GetStatementRange(token);
            }
            catch (Exception err)
            {
                Helpers.Logger(err);
            }
            

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("`{0}` range is {1}",
                token.Type,
                range.TokenString);

            range.Print();
            Console.ForegroundColor = ConsoleColor.White;

            return range;
        }
        
        TokenCollection ParseFunction(Token token)
        {
            var range = GetFunctionRange(token);

            if (range.List.Count != 4)
            {
                throw SqlException.FunctionException;
                // Cauze a SQL function should consist of four tokens:
                // <func_name> <parenthes> <argument> <parenthes>
            }

            Console.WriteLine(range.TokenValueString);

            bool[] check = {
                // For understanding this magic(matching `Type` instead of `Value`) see the `Function`'s pattern reference
                Config.Patterns.Function.IsMatch(range.Get(0).Type),    
                Config.Patterns.OpenParenthes.IsMatch(range.Get(1).Value),
                Config.Patterns.Argument.IsMatch(range.TokenValueString),
                Config.Patterns.CloseParenthes.IsMatch(range.Get(3).Value)
            };

            if (check.All(item => item)) // If everything allright...
            {
                this.Tokens = new TokenCollection(this.Tokens.List.Except(range.List).ToList());
                return new TokenCollection(range.List.Where(
                    _token => !Config.Patterns.Parenthes.IsMatch(_token.Value)).ToList());
            }
            else    // ... else let's do panic
            {
                throw SqlException.FunctionException;
            }
        }

        TokenCollection GetFunctionRange(Token token)
        {
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = token.Index;

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

            try
            {
                // To next statement
                while (!Config.Patterns.Statement.IsMatch(Tokens.List[tokenCounter].Type))
                {
                    var currentToken = Tokens.Get(tokenCounter++);
                    if (Config.Patterns.Function.IsMatch(currentToken.Type))
                    {
                        TokenCollection functionTokenRange = null;
                        try
                        {
                            functionTokenRange = ParseFunction(currentToken);
                            //Console.WriteLine("Function `{0}` range is {1}",
                            //    token.Type,
                            //    functionTokenRange.TokenString);
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                        
                        tokenRange.List.AddRange(functionTokenRange.List);
                    }
                    else if (Config.Patterns.Variable.IsMatch(currentToken.Type) ||
                        Config.Patterns.Separator.IsMatch(currentToken.Type))
                    {
                        tokenRange.Add(currentToken);
                    }
                    else
                    {
                        throw SqlException.SelectStatementException;
                    }
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
