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

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var factory = new ExcelQueryFactory(openFileDialog.FileName);
                factory.AddMapping<MetaTagsLine>(m=>m.Url,"URL");
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedDescription, "DESCRIPTION");
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedKeywords, "KEYWORDS");
                factory.AddMapping<MetaTagsLine>(m => m.ExpectedTitle, "TITLE");
                var worksheet = factory.GetWorksheetNames().First();
                var columns = factory.GetColumnNames(worksheet);
                dataGridView1.DataSource = bindingSource1;
                bindingSource1.DataSource = factory.Worksheet<MetaTagsLine>(worksheet).ToList();
            }
        }
    }
}
