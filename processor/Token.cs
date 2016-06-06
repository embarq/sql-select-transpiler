namespace processor
{
    class Token
    {
        public string type;
        public string value;
        public int index;

        public Token(string type, string value, int index)
        {
            this.type = type;
            this.value = value;
            this.index = index;
        }
    }
}
