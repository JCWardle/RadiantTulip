using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public interface ISettingsReader
    {
        void CreatePlayerSizes(Stream input);
    }
}
