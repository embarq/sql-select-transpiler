namespace simple_db
{
    public class Database
    {
        public string name;
        public TableCollection tables;
        public Database()
        {
            tables = new TableCollection();
        }
    }
}
