using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace transpiler
{
    class TokenCollection
    {
        private List<Token> collection;

        /// <summary>
        /// Return `List` data structure instead of TokenCollection
        /// </summary>
        public List<Token> List
        {
            get
            {
                return collection;
            }
        }

        public TokenCollection Add(Token stmt)
        {
            collection.Add(stmt);
            return this;
        }

        public TokenCollection Add(TokenCollection anotherCollection)
        {
            collection.AddRange(anotherCollection.List);
            return this;
        }
        
        public Token Get(int index)
        {
            return collection.ElementAt(index);
        }

        public Token GetByType(string type)
        {
            Token found = collection.Find(token => token.Type.Equals(type));
            if (found.Equals(null))
            {
                throw new NullReferenceException(string.Format("Token with `{0}` type not present", type));
            } 
            else
            {
                return found;
            }
        }

        public Token GetByValue(string value)
        {
            Token found = collection.Find(token => token.Value.Equals(value));
            if (found.Equals(null))
            {
                throw new NullReferenceException(string.Format("Token with `{0}` value not present", value));
            }
            else
            {
                return found;
            }
        }

        public Token GetByIndex(int index)
        {
            Token found = collection.Find(token => token.Index == index);
            if (found.Equals(null))
            {
                throw new NullReferenceException(string.Format("Token with `{0}` index not present", index));
            }
            else
            {
                return found;
            }
        }

        public bool HasType(string type)
        {
            return collection.Any(
                token => new Regex(type, RegexOptions.IgnoreCase).IsMatch(token.Type));
        }

        public TokenCollection()
        {
            collection = new List<Token>();
        }

        public TokenCollection(List<Token> collection)
        {
            this.collection = collection;
        }

        public List<string> TokenList
        {
            get
            {
                return List.Select(token => token.Type).ToList();
            }
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

        public string TokenValueString
        {
            get
            {
                return string.Join("", List.ConvertAll<string>(token => token.Value));
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
