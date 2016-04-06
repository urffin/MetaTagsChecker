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

        private List<string> header { get; set; }

        public CSV(char separator = ';', bool hasHeader = false)
        {
            HasHeader = hasHeader;
            Separator = separator;
        }
        public async Task<IEnumerable<string[]>> Load(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await Load(reader);
            }
        }
        public async Task<IEnumerable<string[]>> Load(TextReader reader)
        {
            string line;
            if (HasHeader)
            {
                header = await GetHeader(reader);
            }
            while ((line = await reader.ReadLineAsync()) != null)
            {

            }
        }

        private async Task<List<string>> GetHeader(TextReader reader)
        {
            var line = await reader.ReadLineAsync();
            return GetColumns(line).ToList();


        }

        private IEnumerable<string> GetColumns(string line)
        {
            throw new NotImplementedException();
        }
    }
}
