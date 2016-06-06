﻿using System.Collections.Generic;

namespace simple_db
{
    public class Table
    {
        public string name;
        public string[] fields;
        public List<TableRow> rows;

        /// <summary>Add row to the current table</summary>
        /// <returns>Ref to the last row in rows</returns
        public TableRow AddRow(List<KeyValuePair<string, string>> row)
        {
            TableRow newRow = new TableRow();
            foreach (KeyValuePair<string, string> col in row)
            {
                newRow.AddColumn(col.Key, col.Value);
            }
            rows.Add(newRow);
            return newRow;
        }

        public TableRow GetRow(int id)
        {
            return rows[id];
        }

        /// <param name="name">Table name</param>
        /// <param name="fields">Heading labels</param>
        public Table(string name, string[] fields)
        {
            this.fields = fields;
            this.name = name;
            this.rows = new List<TableRow>();
        }
    }
}
