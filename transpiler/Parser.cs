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
            TokenCollection statementRange = new TokenCollection();

            try
            {
                statementRange = GetStatementRange(token);
            }
            catch (Exception err)
            {
                throw err;
            }
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("`{0}` statement range is {1}",
                token.Type,
                statementRange.TokenString);

            statementRange.Print();
            Console.ForegroundColor = ConsoleColor.White;

            return statementRange;
        }

        TokenCollection GetStatementRange(Token token)
        {
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = token.Index + 1;

            try
            {
                while (!Config.Patterns.Statement.IsMatch(Tokens.GetByIndex(tokenCounter).Type))
                {
                    var currentToken = Tokens.GetByIndex(tokenCounter++);

                    if (token.Type.Equals("where_stmt"))
                    {
                        try
                        {
                            tokenRange.Add(ParseWhereStatement(currentToken));
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else if (Config.Patterns.Function.IsMatch(currentToken.Type))
                    {
                        try
                        {
                            tokenRange.Add(ParseFunction(currentToken));
                            tokenCounter += 3;
                        }
                        catch (Exception err)
                        {
                            throw err;
                        }
                    }
                    else if (Config.Patterns.Variable.IsMatch(currentToken.Value) ||
                        Config.Patterns.Arythmetics.IsMatch(currentToken.Value) ||
                        Config.Patterns.Comparators.IsMatch(currentToken.Value))
                    {
                        tokenRange.Add(currentToken);
                    }
                    else if (Config.Patterns.Separator.IsMatch(currentToken.Value))
                    {
                        continue;
                    }
                    else
                    {
                        throw SqlException.SelectStatementException;
                    }
                }
            }
            // This two catches are going to catch overrange
            catch (NullReferenceException)
            {
                return tokenRange;
            }
            catch (ArgumentOutOfRangeException)
            {
                return tokenRange;
            }

            return tokenRange;
        }

        TokenCollection ParseWhereStatement(Token token)
        {
            var clauseRange = GetClauseRange(token);
        }

        TokenCollection GetClauseRange(Token token)
        {
            var tokenRange = new TokenCollection();

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

            try
            {
                for (int i = token.Index; i < token.Index + Config.functionStatementLength; i++)
                {
                    tokenRange.Add(Tokens.GetByIndex(i));
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
