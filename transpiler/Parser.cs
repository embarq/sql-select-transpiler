using System.Collections.Generic;
using System.Linq;
using System;

namespace transpiler
{
    class Parser
    {
        TokenCollection Tokens;

        public Parser(TokenCollection tokens)
        {
            this.Tokens = tokens;

            if (!(tokens.HasType("select") && tokens.HasType("from")))
            {
                throw SqlException.StatementsException;
            }

            // Парсинг начинается с statement'ов
            foreach (Token token in this.Tokens.List)
            {
                if (Config.Patterns.Statement.IsMatch(token.Type))
                {
                    try
                    {
                        ParseStatement(token);
                    }
                    catch (Exception err)
                    {
                        Helpers.Logger(err);
                        throw err;
                    }
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
                throw err;
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

        TokenCollection GetStatementRange(Token token)
        {
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = token.Index + 1;

            try
            {
                // To next statement
                while (!Config.Patterns.Statement.IsMatch(Tokens.Get(tokenCounter).Type))
                {
                    var currentToken = Tokens.Get(tokenCounter++);

                    if (Config.Patterns.Function.IsMatch(currentToken.Type))
                    {
                        TokenCollection functionTokenRange = null;

                        try
                        {
                            functionTokenRange = ParseFunction(currentToken);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(functionTokenRange.TokenString);
                            Console.ForegroundColor = ConsoleColor.White;
                            tokenRange.List.AddRange(functionTokenRange.List);  // Replace natively
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
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

        TokenCollection ParseFunction(Token token)
        {
            var range = GetFunctionRange(token);

            if (range.List.Count != 4)
            {
                // a SQL function should consist of four tokens:
                // <func_name> <parenthes> <argument> <parenthes>
                throw SqlException.FunctionException;
            }

            bool[] check = {
                Config.Patterns.Function.IsMatch(range.Get(0).Type),    
                Config.Patterns.OpenParenthes.IsMatch(range.Get(1).Value),
                Config.Patterns.Argument.IsMatch(range.TokenValueString),
                Config.Patterns.CloseParenthes.IsMatch(range.Get(3).Value)
            };

            if (check.All(item => item))
            // If everything allright updating tokens sub-set
            // and return a function name and it's argument. Else do panic:)
            {
                range.Get(2).Type = "argument";

                Tokens = new TokenCollection(Tokens.List.Except(range.List).ToList());
                return new TokenCollection(range.List.Where(
                    _token => !Config.Patterns.Parenthes.IsMatch(_token.Value)).ToList());
            }
            else
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
                while (Tokens.Get(tokenCounter).Type != "separator")
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
