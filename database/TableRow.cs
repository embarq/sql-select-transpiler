using System.Collections.Generic;

namespace simple_db
{
    public class TableRow
    {
        private List<TableColumn> columns;

        public TableRow AddColumn(TableColumn tableColumn)
        {
            columns.Add(tableColumn);
            return this;
        }

        public TableRow AddColumn(string field, dynamic data)
        {
            columns.Add(new TableColumn(this.columns.Count, field, data));
            return this;
        }

        public List<TableColumn> GetColumns()
        {
            return columns;
        }

        public TableRow()
        {
            columns = new List<TableColumn>();
        }
    }
}
