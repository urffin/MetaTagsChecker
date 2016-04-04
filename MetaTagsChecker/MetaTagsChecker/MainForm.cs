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
using MetaTagsCheckerLib;

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
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedH1, "H1");
                var worksheet = factory.GetWorksheetNames().First();
                var columns = factory.GetColumnNames(worksheet);
                dataGridView1.DataSource = bindingSource1;
                metatags = factory.Worksheet<MetaTagsLine>(worksheet).ToList();
                bindingSource1.DataSource = metatags;
            }
        }

        private async void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Task.WhenAll(metatags.Select(m => new MetaTagsCheckerLib.MetaTagsChecker(m).Check()));

            dataGridView1.Refresh();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            const int countExpectedFields = 4;
            const int errorCellIndex = 9;

            if (e.ColumnIndex > countExpectedFields)
            {
                var currentValue = Convert.ToString(e.Value).Trim();
                if (e.ColumnIndex == errorCellIndex && !string.IsNullOrWhiteSpace(currentValue))
                {
                    e.CellStyle.BackColor = Color.Red;
                    return;
                }
                var expectedValue = Convert.ToString(((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex - countExpectedFields].Value).Trim();
                if (!string.IsNullOrWhiteSpace(currentValue))
                    e.CellStyle.BackColor = (currentValue == expectedValue) ? Color.LightGreen : Color.Red;
            }
        }
    }
}
