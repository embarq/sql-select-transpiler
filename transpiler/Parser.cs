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

            if (!(tokens.HasType("select") && tokens.HasType("from") && tokens.HasType("eof")))
            {
                throw SqlException.StatementsException;
            }

            // Парсинг начинается с statement'ов
            foreach (Token token in this.Tokens.List)
            {
                if (token.IsStatement)
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
                while (!Tokens.GetByIndex(tokenCounter).IsStatement)
                {
                    var currentToken = Tokens.GetByIndex(tokenCounter++);

                    if (currentToken.IsEOF)
                    {
                        return tokenRange;
                    }
                    else if (currentToken.IsStatementType("where"))
                    {
                        Console.WriteLine("Where statement");
                        try
                        {
                            tokenRange.Add(ParseWhereStatement(currentToken));
                        }
                        catch (InvalidOperationException err)
                        {
                            throw err;
                        }
                    }
                    else if (currentToken.IsFunction)
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
                    else if (currentToken.IsVariable || currentToken.IsArythmetic || currentToken.IsComparator)
                    {
                        tokenRange.Add(currentToken);
                    }
                    else if (currentToken.IsSeparator)
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
            TokenCollection clauseExpression = null;
            try
            {
                clauseExpression = ParseClauseExpression(token);
            }
            catch (InvalidOperationException)
            {
                throw SqlException.WhereStatementException;
            }

            return clauseExpression;
        }

        TokenCollection ParseClauseExpression(Token token)
        {
            var clauseRange = GetClauseRange(token);
            var comparator = clauseRange.List.Find(_token => _token.IsComparator);
            var left = clauseRange.Get(0);
            var right = clauseRange.Get(2);

            if (comparator.Equals(null))
            {
                throw SqlException.ClauseExpressionException;
            }

            return clauseRange;
        }

        TokenCollection GetClauseRange(Token token)
        {
            var tokenRange = new TokenCollection();
            int tokenCounter = token.Index;

            try {
                while (!Tokens.GetByIndex(tokenCounter).IsStatement || !Tokens.GetByIndex(tokenCounter).IsEOF)
                {
                    var currentToken = Tokens.GetByIndex(tokenCounter++);

                    if (currentToken.IsVariable ||
                        currentToken.IsArythmetic ||
                        currentToken.IsDigit ||
                        currentToken.IsComparator ||
                        currentToken.IsParenthes)
                    {
                        tokenRange.Add(currentToken);
                    }
                    else
                    {
                        throw SqlException.WhereStatementException;
                    }
                }
            }
            catch (NullReferenceException)
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
            {
                range.Get(2).Type = "argument";
                
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
