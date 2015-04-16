using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public interface IGameSetupViewModel
    {
        Ground Ground { get; set; }
        string PositionalData { get; set; }
        bool AdvancedSettings { get; set; }
        bool Loading { get; set; }
        int LoadingProgress { get; set; }
        GroundType GroundType { get; set; }
        ICommand AdvancedSettingsToggle { get; }
        ICommand SelectedGroundChangedCommand { get; }
        ICommand SelectedGroundTypeChangedCommand { get; }
        ICommand StartGameCommand { get; }
        IEnumerable<GroundType> GroundTypes { get; }
        IList<Ground> SelectableGrounds { get; }
    }
}
