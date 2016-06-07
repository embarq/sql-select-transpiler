using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace processor
{
    class TokenCollection
    {
        List<Token> collection;
        int currentIndex;

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

        /// <summary>
        /// Return `List` data structure instead of TokenCollection
        /// </summary>
        public List<Token> Get()
        {
            return this.collection;
        }

        public TokenCollection GetCollection()
        {
            return this;
        }

        /// <summary>
        /// Return token which has current type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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

        public Token Next()
        {
            return this.collection[++this.currentIndex];
        }

        public Token Current()
        {
            return this.collection[this.currentIndex];
        }

        public TokenCollection()
        {
            this.collection = new List<Token>();
            this.currentIndex = -1;
        }

        public TokenCollection(List<Token> collection)
        {
            this.collection = collection;
            this.currentIndex = 0;
        }
    }
}
