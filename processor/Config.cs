using System.Linq;
using System.Text.RegularExpressions;

namespace processor
{
    public class Config
    {
        struct patterns
        {
            public static string Statements = @"(?<select_stmt>select)|(?<from_stmt>from)|(?<group_by_stmt>group\sby)|(?<where_stmt>where)|(?<having_stmt>having)";
            public static string Functions = @"(?<max_func>max)|(?<min_func>min)|(?<count_func>count)|(?<sum_func>sum)|(?<avg_func>avg)|(?<length_func>length)";
            public static string Alias = @"(?<alias>as)";
            public static string Separator = @"(?:\s*(?<separator>\,)\s*)";
            public static string OpenParenthes = @"(?<open_parenthes>\()";
            public static string CloseParenthes = @"(?<close_parenthes>\))";
            public static string Digits = @"(?<digit>\d+)";
            public static string Variable = @"(?<variable>(?:\w{1,18}))";
            public static string Comparators = @"(?<equals>\=)|(?<not_equals>\!\=)|(?<less>\<)|(?<greater>\>)";
            public static string Arythmetics = @"(?<add>\+)|(?<substract>\-)";
            public static string Statement = @"(?:(\w+)(?:_)(?:stmt))|(?:(?:stmt)(?:_)(\w+))";
            public static string Function = @"(?:(\w+)(?:_)(?:func))|(?:(?:func)(?:_)(\w+))";

            static string[] Lexems = { Statements, Functions, Alias, Separator, OpenParenthes, CloseParenthes, Digits, Variable, Comparators, Arythmetics };
            public static string Lexer = string.Join("|", Lexems);
        }

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
            public static Regex Lexer = new Regex(patterns.Lexer, RegexOptions.IgnoreCase);
            public static Regex Statement = new Regex(patterns.Statement, RegexOptions.IgnoreCase);
            public static Regex Function = new Regex(patterns.Function, RegexOptions.IgnoreCase);
            public static Regex Variable = new Regex(patterns.Variable, RegexOptions.IgnoreCase);
            public static Regex Digits = new Regex(patterns.Digits);
            public static Regex OpenParenthes = new Regex(patterns.OpenParenthes);
            public static Regex CloseParenthes = new Regex(patterns.CloseParenthes);
            public static Regex Separator = new Regex(patterns.Separator);
            public static Regex Comparators = new Regex(patterns.Comparators);
            public static Regex Arythmetics = new Regex(patterns.Arythmetics);
        }
    }
}