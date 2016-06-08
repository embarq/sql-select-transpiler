using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace processor
{
    class TokenCollection
    {
        List<Token> collection;

        /// <summary>
        /// Return `List` data structure instead of TokenCollection
        /// </summary>
        public List<Token> List
        {
            get
            {
                return this.collection;
            }
        }

        public bool HasType(string type)
        {   
            return this.collection.Any(
                token => new Regex(type, RegexOptions.IgnoreCase)
                .Match(Config.Patterns.Statement.Match(token.Type).Value).Value != "");
        }

        public TokenCollection Add(Token stmt)
        {
            this.collection.Add(stmt);
            return this;
        }

        public Token Get(int index)
        {
            return this.collection.ElementAt(index);
        }

        /// <summary>
        /// Return token which has current type
        /// </summary>
        public Token GetByType(string type)
        {
            Token found = this.collection.Find(token => token.Type.Equals(type));
            return found != null ? found : null;
        }

        public Token GetByValue(string value)
        {
            Token found = this.collection.Find(token => token.Value.Equals(value));
            return found != null ? found : null;
        }

        public TokenCollection()
        {
            this.collection = new List<Token>();
        }

        public TokenCollection(List<Token> collection)
        {
            this.collection = collection;
        }

        public string TokenString
        {
            get
            {
                return string.Format("<{0}>", 
                    string.Join(" ", 
                        List.ConvertAll<string>(
                            token => string.Format("<{0}>", token.Type))));
            }
        }

        public void Print()
        {
            foreach (var token in List)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[{0,2}]", token.Index);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0,15}", token.Value);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" => ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(token.Type);
                Console.WriteLine();
            }
        }
    }
}
