using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameSetupViewModel
    {
        OberservableGround Ground { get; set; }
        string PositionalData { get; set; }
        bool AdvancedSettings { get; set; }
        ICommand AdvancedSettingsToggle { get; }
        IEnumerable<GroundType> GroundTypes { get; }
    }
}
