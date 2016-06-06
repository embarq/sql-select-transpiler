using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace simple_db
{
    public class TableCollection
    {
        public Dictionary<string, Table> storage;

        /// <summary>Return table by name</summary>
        public Table Get(string name)
        {
            return storage[name];
        }

        /// <summary>Add new table and return ref to this table</summary>
        public Table Add(string tableName)
        {
            storage.Add(tableName, new Table(tableName, null));
            return Get(tableName);
        }

        /// <summary>Add new table and return ref to this table</summary>
        public Table Add(Table table)
        {
            storage.Add(table.name, table);
            return Get(table.name);
        }

        public Table Import(string fileName, string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = new Regex(@"(\w+[-_]?\w+)").Match(fileName).Value;
            }

            var importData = JsonImport.From(fileName);

            List<string> fields = new List<string>();
            foreach(var data in importData[0])
            {
                fields.Add(data.Key);
            }

            Table table = new Table(tableName, fields.ToArray());

            foreach (var data in importData)
            {
                table.AddRow(data);
            }
            
            return Add(table);
        }

        public TableCollection() {
            storage = new Dictionary<string, Table>();
        }
    }
}
