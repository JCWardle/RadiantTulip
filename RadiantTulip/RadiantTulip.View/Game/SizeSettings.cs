using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Game
{
    public class SizeSettings : ISizeSettings
    {

        public IReadOnlyDictionary<Size, int> ReadSizeSettings(Stream settings)
        {
            var result = new Dictionary<Size, int>()
            {
                { Size.Small, 10 },
                { Size.Medium, 20 },
                { Size.Large, 30 },
                { Size.ExtraLarge, 40 }
            };

            if(settings == null)
                return new ReadOnlyDictionary<Size, int>(result);

            using(var reader = new JsonTextReader(new StreamReader(settings)))
            {
                var scaleSection = JToken.ReadFrom(reader)["Scale"];

                result[Size.Small] = scaleSection["S"] == null ? 10 : scaleSection["S"].Value<int>();
                result[Size.Medium] = scaleSection["M"] == null ? result[Size.Small] + 10 : scaleSection["M"].Value<int>();
                result[Size.Large] = scaleSection["L"] == null ? result[Size.Medium] + 10 : scaleSection["L"].Value<int>();
                result[Size.ExtraLarge] = scaleSection["XL"] == null ? result[Size.Large] + 10 : scaleSection["XL"].Value<int>();
            }

            return new ReadOnlyDictionary<Size, int>(result);
        }
    }
}
