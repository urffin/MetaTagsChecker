using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaTagsCheckerLib
{
    public class CSV
    {
        public bool HasHeader { get; private set; }
        public char Separator { get; private set; }

        public char Quote { get; private set; }
        private List<string> header { get; set; }



        public CSV(char separator = ';', bool hasHeader = false, char quote = '"')
        {
            HasHeader = hasHeader;
            Separator = separator;
            Quote = quote;
        }
        public IEnumerable<IEnumerable<string>> Load(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Load(reader);
            }
        }
        public IEnumerable<IEnumerable<string>> Load(TextReader reader)
        {
            string line;
            if (HasHeader)
            {
                header = GetHeader(reader);
            }
            while ((line = reader.ReadLine()) != null)
            {
                yield return GetColumns(line);
            }
        }

        private List<string> GetHeader(TextReader reader)
        {
            var line = reader.ReadLine();
            if (!string.IsNullOrEmpty(line))
                return GetColumns(line).ToList();

            return new List<string>();

        }

        private IEnumerable<string> GetColumns(string line)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < line.Length)
            {
                if (line[i] == Quote)
                    yield return GetQuotedColumn(line, ref i);
                else
                    yield return GetUnQuotedColumn(line, ref i);
            }
        }

        private string GetUnQuotedColumn(string line, ref int i)
        {
            StringBuilder sb = new StringBuilder();
            for (; i < line.Length && line[i] != Separator; i++)
            {
                sb.Append(line[i]);
            }
            i += 1;//drop separator
            return sb.ToString();
        }

        private string GetQuotedColumn(string line, ref int i)
        {
            StringBuilder sb = new StringBuilder();
            for (i = i + 1; i < line.Length; i++)
            {
                if (line[i] == Quote && (i == line.Length - 1 || line[i + 1] == Separator)) break;

                if (line[i] == Quote && line[i + 1] == Quote)
                {
                    i += 1;
                }
                sb.Append(line[i]);
            }
            i += 1;//drop quote
            i += 1;//drop separator
            return sb.ToString();
        }
    }
}
