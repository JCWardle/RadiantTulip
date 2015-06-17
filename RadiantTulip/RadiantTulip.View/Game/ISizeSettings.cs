using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Game
{
    public interface ISizeSettings
    {
        IReadOnlyDictionary<Size, int> ReadSizeSettings(Stream stream);
    }
}
