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

        public bool IsEOF
        {
            get
            {
                return Config.Patterns.EOF.IsMatch(this.Value);
            }
        }

        public bool IsStatement
        {
            get
            {
                return Config.Patterns.Statement.IsMatch(this.Type);
            }
        }

        public bool IsWhereStatement
        {
            get
            {
                return Type.Equals("where_stmt");
            }
        }

        public bool IsFunction
        {
            get
            {
                return Config.Patterns.Function.IsMatch(this.Type);
            }
        }

        public bool IsVariable
        {
            get
            {
                return Config.Patterns.Variable.IsMatch(this.Value);
            }
        }

        public bool IsDigit
        {
            get
            {
                return Config.Patterns.Digits.IsMatch(this.Value);
            }
        }

        public bool IsArythmetic
        {
            get
            {
                return Config.Patterns.Arythmetics.IsMatch(this.Value);
            }
        }

        public bool IsParenthes
        {
            get
            {
                return Config.Patterns.Parenthes.IsMatch(this.Value);
            }
        }

        public bool IsComparator
        {
            get
            {
                return Config.Patterns.Comparators.IsMatch(this.Value);
            }
        }

        public bool IsSeparator
        {
            get
            {
                return Config.Patterns.Separator.IsMatch(this.Value);
            }
        }
    }
}
