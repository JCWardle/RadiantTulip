using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class JsonGroundReader : IGroundReader
    {
        public IList<Ground> ReadGrounds(IList<Stream> input)
        {
            var result = new List<Ground>();

            foreach(var stream in input)
            { 
                using(var reader = new JsonTextReader(new StreamReader(stream)))
                {
                    var obj = JToken.ReadFrom(reader);
                    var ground = new Ground()
                    {
                        CentreLatitude = (double)JsonConvert.DeserializeObject(obj["CentreLatitude"].ToString(), typeof(double)),
                        CentreLongitude = (double)JsonConvert.DeserializeObject(obj["CentreLongitude"].ToString(), typeof(double)),
                        Height = (int)JsonConvert.DeserializeObject(obj["Height"].ToString(), typeof(int)),
                        Width = (int)JsonConvert.DeserializeObject(obj["Width"].ToString(), typeof(int)),
                        Padding = (int)JsonConvert.DeserializeObject(obj["Padding"].ToString(), typeof(int)),
                        Name = obj["Name"].ToString(),
                        Type = (GroundType)Enum.Parse(typeof(GroundType), obj["Type"].ToString())
                    };

                    result.Add(ground);
                }
            }

            return result;
        }
    }
}
