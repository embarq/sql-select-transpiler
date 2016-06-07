using System.Text.RegularExpressions;

namespace Config
{
    class Patterns
    {
        public static Regex lexer = new Regex(@"(?<select_stmt>select)|(?<from_stmt>from)|(?<group_by_stmt>group\sby)|(?<where_stmt>where)|(?<having_stmt>having)|(?<alias>as)|(?<max_func>max)|(?<min_func>min)|(?<count_func>count)|(?<sum_func>sum)|(?<avg_func>avg)|(?<length_func>length)|(?:\s*(?<separator>\,)\s*)|(?<digit>\d)|(?<variable>(?:\w{1,18}))|(?<equals>\=)|(?<not_equals>\!\=)|(?<less>\<)|(?<greater>\>)|(?<open_parenthes>\()|(?<close_parenthes>\))|(?<add>\+)|(?<substract>\-)", RegexOptions.IgnoreCase);
        public static Regex stmt = new Regex(@"(?:(\w+)(?:_)(?:stmt))|(?:(?:stmt)(?:_)(\w+))", RegexOptions.IgnoreCase);
    }
}
