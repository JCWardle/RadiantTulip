using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Input
{
    public interface ISpatialReader
    {
        List<Team> GetTeams(Stream stream);
    }
}
