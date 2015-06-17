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
            throw new NotImplementedException();
        }
    }
}
