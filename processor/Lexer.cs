using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace processor
{
    class Lexer
    {
        TokenCollection tokens;
        string expr;
        public static string mainPattern = @"(?<select_stmt>select)|(?<from_stmt>from)|(?<group_by_stmt>group\sby)|(?<where_stmt>where)|(?<having_stmt>having)|(?<alias>as)|(?<max_func>max)|(?<min_func>min)|(?<count_func>count)|(?<sum_func>sum)|(?<avg_func>avg)|(?<length_func>length)|(?:\s*(?<separator>\,)\s*)|(?<digit>\d)|(?<variable>(?:\w{1,18}))|(?<equals>\=)|(?<not_equals>\!\=)|(?<less>\<)|(?<greater>\>)|(?<open_parenthes>\()|(?<close_parenthes>\))|(?<add>\+)|(?<substract>\-)";

        public Lexer(string expr)
        {
            this.expr = expr;
            tokens = new TokenCollection();
        }

        public TokenCollection Analyze()
        {
            Regex pattern = new Regex(mainPattern, RegexOptions.IgnoreCase);

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
