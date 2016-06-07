using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace processor
{
    class Lexer
    {
        TokenCollection tokens;
        string expr;

        public Lexer(string expr)
        {
            this.expr = expr;
            tokens = new TokenCollection();
        }

        public TokenCollection Analyze()
        {
            Regex pattern = Config.Patterns.Lexer;

            var tokens = new TokenCollection();
            string[] groupNames = pattern.GetGroupNames();
            int tokenIndex = 0;

            foreach (Match match in pattern.Matches(expr))
            {
                foreach (string key in groupNames)
                {
                    string value = match.Groups[key].Value;
                    if (!string.IsNullOrEmpty(value) && key != "0")
                    {
                        tokens.Add(new Token(key, value, tokenIndex++));
                    }
                }
            }

            return tokens;
        }
    }
}
