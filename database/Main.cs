using System.Windows.Forms;
using System.Collections.Generic;

namespace simple_db
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            OutputTable();
        }

        public void OutputTable()
        {
            Database db = new Database();
            Table table = db.tables.Import(@"../../apps.json", null);

            foreach (string heading in table.fields)
            {
                tableView.Columns.Add(heading);
            }

            foreach (ColumnHeader columnHeader in tableView.Columns)
            {
                columnHeader.Width = -2;
            }

            foreach (TableRow row in table.rows)
            {
                List<TableColumn> col = row.GetColumns();
                ListViewItem item = new ListViewItem(col[0].data);
                for (int i = 1; i < col.Count; i++)
                {
                    item.SubItems.Add(col[i].data);
                }
                tableView.Items.Add(item);
            }
        }
    }
}
