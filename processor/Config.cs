using System;
using System.Text.RegularExpressions;

namespace processor
{
    public class Config
    {
        public struct Patterns
        {
            /// <summary>
            /// Match expression's grammar
            /// </summary>
            /// <returns>
            /// Matches: 
            /// Statements:  select, from, group_by, where, having, alias, 
            /// Functions:   max, min, count, sum, avg, length, 
            /// Literals:    digit, variable, 
            /// Punctuals:   separator, open, close,
            /// Comparators: equals, not_equals, less, greater, 
            /// Arythmetic:  add, substract
            /// </returns>
            public static Regex Lexer = new Regex(@"(?<select_stmt>select)|(?<from_stmt>from)|(?<group_by_stmt>group\sby)|(?<where_stmt>where)|(?<having_stmt>having)|(?<alias>as)|(?<max_func>max)|(?<min_func>min)|(?<count_func>count)|(?<sum_func>sum)|(?<avg_func>avg)|(?<length_func>length)|(?:\s*(?<separator>\,)\s*)|(?<digit>\d)|(?<variable>(?:\w{1,18}))|(?<equals>\=)|(?<not_equals>\!\=)|(?<less>\<)|(?<greater>\>)|(?<open_parenthes>\()|(?<close_parenthes>\))|(?<add>\+)|(?<substract>\-)", RegexOptions.IgnoreCase);
            public static Regex Statement = new Regex(@"(?:(\w+)(?:_)(?:stmt))|(?:(?:stmt)(?:_)(\w+))", RegexOptions.IgnoreCase);
            public static Regex Function = new Regex(@"(?:(\w+)(?:_)(?:func))|(?:(?:func)(?:_)(\w+))", RegexOptions.IgnoreCase);
        }
    }
}
