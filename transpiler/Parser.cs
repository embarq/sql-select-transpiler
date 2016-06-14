using System.Linq;
using System;

namespace transpiler
{
    class Parser
    {
        private TokenCollection Tokens;
        public TokenCollection parsedTokens;
        public bool Status { get; }

        public Parser(TokenCollection tokens)
        {
            Tokens = tokens;
            parsedTokens = new TokenCollection();

            if (!tokens.HasType("select"))
            {
                throw SqlException.InvalidSelectStatementSyntax;
            }
            else if (!tokens.HasType("from"))
            {
                throw SqlException.MissingFromStatement;
            }
            else if (!tokens.HasType("eof"))
            {
                throw SqlException.MissingEndOfLineToken;
            }
            
            foreach (Token token in Tokens.List)
            {
                try
                {
                    if (token.IsStatement)
                    {
                        parsedTokens.Add(ParseStatement(token));
                    }
                }
                catch (Exception exception)
                {
                    Status = false;
                    throw exception;
                }
            }
            
            Status = true;
        }

        TokenCollection ParseStatement(Token token)
        {
            TokenCollection statementRange = new TokenCollection();

            try
            {
                if (token.IsWhereStatement)
                {
                    statementRange = ParseClauseExpression(token);
                }
                else
                {
                    statementRange = GetStatementRange(token);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
            return statementRange;
        }

        TokenCollection GetStatementRange(Token token)
        {
            TokenCollection tokenRange = new TokenCollection();
            int tokenCounter = token.Index + 1;

            try
            {
                Token currentToken = null;

                do
                {
                    currentToken = Tokens.GetByIndex(tokenCounter++);

                    if (currentToken.IsEOF)
                    {
                        return tokenRange;
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
                    else if (currentToken.IsVariable)
                    {
                        Token nextToken = Tokens.GetByIndex(tokenCounter + 1);

                        if (nextToken.IsSeparator || nextToken.IsVariable || nextToken.IsStatement || nextToken.IsEOF)
                        {
                            tokenRange.Add(currentToken);
                        }
                        
                    }
                    else if (currentToken.IsSeparator)
                    {
                        continue;
                    }
                    else
                    {
                        throw SqlException.InvalidSelectStatementSyntax;
                    }
                } while (!currentToken.IsStatement);
            }
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

        TokenCollection ParseClauseExpression(Token token)
        {
            var clauseRange = GetClauseRange(token);
            
            if (!clauseRange.Get(1).IsComparator)
            {
                throw SqlException.InvalidClauseExpressionSyntax;
            }

            var temp = clauseRange.List[0];
            clauseRange.List[0] = clauseRange.List[1];
            clauseRange.List[1] = temp;

            return clauseRange;
        }

        TokenCollection GetClauseRange(Token token)
        {
            var tokenRange = new TokenCollection();
            int tokenCounter = token.Index + 1;

            try {
                while (!Tokens.GetByIndex(tokenCounter).IsStatement)
                {
                    var currentToken = Tokens.GetByIndex(tokenCounter++);

                    if (currentToken.IsEOF)
                    {
                        return tokenRange;
                    }
                    else if (currentToken.IsVariable ||
                        currentToken.IsArythmetic ||
                        currentToken.IsDigit ||
                        currentToken.IsComparator ||
                        currentToken.IsParenthes)
                    {
                        tokenRange.Add(currentToken);
                    }
                    else
                    {
                        throw SqlException.IvalidWhereStatementSyntax;
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
                throw SqlException.InvalidFunctionSyntax;
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
                throw SqlException.InvalidFunctionSyntax;
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
