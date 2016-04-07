using System;
using System.IO;
using System.Linq;
using MetaTagsCheckerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaTagsCheckerTest
{
    [TestClass]
    public class TestCSV
    {
        [TestMethod]
        public void GetColumn()
        {
            string csv = "a;b;c";
            using (var reader = new StringReader(csv))
            {
                var rows = new CSV().Load(reader).ToList();

                Assert.AreEqual(1, rows.Count, "Readed not single line");
                var columns = rows[0].ToList();
                Assert.AreEqual(3, columns.Count, "Columns count wrong");
                Assert.AreEqual("a", columns[0], "Wrong value in columns: 0");
                Assert.AreEqual("b", columns[1], "Wrong value in columns: 1");
                Assert.AreEqual("c", columns[2], "Wrong value in columns: 2");

            }
        }
        [TestMethod]
        public void GetEmptyColumn()
        {
            string csv = "a;;c";
            using (var reader = new StringReader(csv))
            {
                var rows = new CSV().Load(reader).ToList();

                Assert.AreEqual(1, rows.Count, "Readed not single line");
                var columns = rows[0].ToList();
                Assert.AreEqual(3, columns.Count, "Columns count wrong");
                Assert.AreEqual("a", columns[0], "Wrong value in columns: 0");
                Assert.IsTrue(string.IsNullOrEmpty(columns[1]), "Not empty column: 1");
                Assert.AreEqual("c", columns[2], "Wrong value in columns: 2");

            }
        }
        [TestMethod]
        public void GetQuotedColumn()
        {
            string csv = "a;\"d;f\";c";
            using (var reader = new StringReader(csv))
            {
                var rows = new CSV().Load(reader).ToList();

                Assert.AreEqual(1, rows.Count, "Readed not single line");
                var columns = rows[0].ToList();
                Assert.AreEqual(3, columns.Count, "Columns count wrong");
                Assert.AreEqual("a", columns[0], "Wrong value in columns: 0");
                Assert.AreEqual("d;f",columns[1], "Wrong reading quoted value in column: 1");
                Assert.AreEqual("c", columns[2], "Wrong value in columns: 2");

            }
        }
        [TestMethod]
        public void GetQuotedColumn_QuotedValue()
        {
            string csv = "a;\"\"\"as\"\"a\";c";
            using (var reader = new StringReader(csv))
            {
                var rows = new CSV().Load(reader).ToList();

                Assert.AreEqual(1, rows.Count, "Readed not single line");
                var columns = rows[0].ToList();
                Assert.AreEqual(3, columns.Count, "Columns count wrong");
                Assert.AreEqual("a", columns[0], "Wrong value in columns: 0");
                Assert.AreEqual("\"\"as\"\"a", columns[1], "Wrong reading quoted value in column: 1");
                Assert.AreEqual("c", columns[2], "Wrong value in columns: 2");

            }
        }
    }
}
