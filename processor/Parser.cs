using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace processor
{
    class Parser
    {
        /// <summary>
        /// Parse current SQL Expression Lexer Map
        /// </summary>
        public static void Parse(TokenCollection tokens)
        {
            Console.WriteLine("Parser started...");
            if (!(tokens.HasType("select") && tokens.HasType("from")))
            {
                throw new ArgumentException("`SELECT` or `FROM` statement's missing", "tokens");
                
            }
        }
    }
}
