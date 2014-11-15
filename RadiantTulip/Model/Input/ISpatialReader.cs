using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Input
{
    internal interface ISpatialReader
    {
        internal List<Team> GetTeams(Stream stream);
    }
}
