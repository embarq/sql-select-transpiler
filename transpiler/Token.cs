namespace transpiler
{
    class Token
    {
        public string Type;
        public string Value;
        public int Index;

        public Token(string type, string value, int index)
        {
            Type = type;
            Value = value;
            Index = index;
        }
    }
}
