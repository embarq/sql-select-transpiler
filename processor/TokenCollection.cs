using System.Collections.Generic;

namespace processor
{
    class TokenCollection
    {
        List<Token> collection;
        int currentIndex;

        public TokenCollection Add(Token stmt)
        {
            this.collection.Add(stmt);
            return this;
        }

        public List<Token> GetList()
        {
            return this.collection;
        }

        public TokenCollection GetCollection()
        {
            return this;
        }

        public Token GetByType(string type)
        {
            foreach (Token stmt in this.collection)
            {
                if (stmt.type == type)
                {
                    return stmt;
                }
            }
            return null;
        }

        public Token GetByValue(string value)
        {
            foreach (Token stmt in this.collection)
            {
                if (stmt.value == value)
                {
                    return stmt;
                }
            }
            return null;
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
