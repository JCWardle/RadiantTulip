using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Game
{
    public interface IDescriptiveArtifact
    {
        string GetName();
        double Calculate(Player player);
    }
}
