using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace MetaTagsCheckerLib
{
    public class MetaTagsChecker
    {
        MetaTagsLine meta;
        public MetaTagsChecker(MetaTagsLine meta)
        {
            this.meta = meta;
        }

        public async Task<MetaTagsLine> Check()
        {
            var request = WebRequest.Create(meta.Url);
            try
            {
                using (var response = await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                {
                    var cs = CQ.Create(stream);
                    meta.CurrentTitle = cs.Find("title").Text();
                    meta.CurrentDescription = cs.Find("meta[name=description]").Attr("content");
                    meta.CurrentKeywords = cs.Find("meta[name=keywords]").Attr("content");
                    meta.CurrentH1 = cs.Find("h1").Text();
                }
            }
            catch (WebException ex)
            {

                meta.Error = ex.Message;
            }
            return meta;
        }
    }
}
