﻿using RadiantTulip.Model;
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
        ICommand AdvancedSettingsToggle { get; }
        ICommand SelectedGroundChanged { get; }
        IEnumerable<GroundType> GroundTypes { get; }
        ObservableCollection<Ground> SelectableGrounds { get; }
    }
}
