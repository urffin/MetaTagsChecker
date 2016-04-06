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

        public CSV(char separator = ';', bool hasHeader = true)
        {
            HasHeader = hasHeader;
            Separator = separator;
        }
        public IEnumerable<string[]> Load(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Load(reader);
            }
        }
        public IEnumerable<string[]> Load(TextReader reader)
        {
            reader.
        }
    }
}
