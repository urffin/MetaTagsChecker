using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqToExcel;

namespace MetaTagsChecker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        List<MetaTagsLine> metatags;

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var factory = new ExcelQueryFactory(openFileDialog.FileName);
                factory.AddMapping<MetaTagsLine>(m => m.Url, "URL");
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedDescription, "DESCRIPTION");
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedKeywords, "KEYWORDS");
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedTitle, "TITLE");
                var worksheet = factory.GetWorksheetNames().First();
                var columns = factory.GetColumnNames(worksheet);
                dataGridView1.DataSource = bindingSource1;
                metatags = factory.Worksheet<MetaTagsLine>(worksheet).ToList();
                bindingSource1.DataSource = metatags;
            }
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            metatags[0].CurrentDescription = "Checked";
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            const int countExpectedFields = 3;
            if (e.ColumnIndex > countExpectedFields)
            {
                var currentValue = Convert.ToString(e.Value);
                var expectedValue = Convert.ToString(((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex - countExpectedFields].Value);
                if (!string.IsNullOrWhiteSpace(currentValue))
                    e.CellStyle.BackColor = (currentValue == expectedValue) ? Color.LightGreen : Color.Red;
            }
        }
    }
}
