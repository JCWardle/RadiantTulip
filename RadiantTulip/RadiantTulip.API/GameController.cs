using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RadiantTulip.API
{
    public class GameController : ApiController
    {
        [HttpPost]
        public async Task Post()
        {
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach(var f in provider.Contents)
            {
                var d = f;
            }
        }
    }
}
