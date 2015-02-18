using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameSetup
    {
        OberservableGround Ground { get; set; }
        string PositionalData { get; set; }
    }
}
