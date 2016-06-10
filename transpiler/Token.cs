namespace transpiler
{
    class Token
    {
        public string Type;
        public string Value;
        public int Index;
        private string Info;


        public Token(string type, string value, int index)
        {
            Type = type;
            Value = value;
            Index = index;
            Info = string.Format("[{0}] {1} <{2}>", Index, Value, Type);
        }
    }
}
