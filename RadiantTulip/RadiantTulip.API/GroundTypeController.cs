using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RadiantTulip.API
{
    public class GroundTypeController : ApiController
    {
        /// <summary>
        /// Retrieves a set of all the ground types that are supported
        /// </summary>
        /// <returns>The grounds</returns>
        public IEnumerable<string> Get()
        {
            var result = new List<string>();
            foreach(var g in Enum.GetValues(typeof(Model.GroundType)))
            {
                var info = g.GetType().GetField(g.ToString());
                var attributes = (DescriptionAttribute[]) info.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    result.Add(attributes.First().Description);
                else
                    result.Add(g.ToString());
            }

            return result;
        }
    }
}
