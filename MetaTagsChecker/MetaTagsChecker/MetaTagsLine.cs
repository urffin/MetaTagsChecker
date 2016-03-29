using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaTagsChecker
{
    public class MetaTagsLine
    {
        public string Url { get; set; }
        public string ExpectedTitle { get; set; }
        public string ExpectedDescription { get; set; }
        public string ExpectedKeywords { get; set; }

        public string CurrentTitle { get; set; }
        public string CurrentDescription { get; set; }
        public string CurrentKeywords { get; set; }
    }
}
