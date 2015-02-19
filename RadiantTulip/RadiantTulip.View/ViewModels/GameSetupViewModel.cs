using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public class GameSetupViewModel : BindableBase, IGameSetupViewModel
    {
        private Ground _ground;
        private string _positionalData;
        private bool _advancedSettings;
        private ICommand _toggleAdvancedSettings;
        private ICommand _selectedGroundChanged;
        private ObservableCollection<Ground> _selectableGrounds;

        public Ground Ground
        {
            get
            {
                return _ground;
            }
            set
            {
                _ground = value;
            }
        }

        public ObservableCollection<Ground> SelectableGrounds
        {
            get
            {
                return _selectableGrounds;
            }
        }

        public string PositionalData
        {
            get
            {
                return _positionalData;
            }
            set
            {
                _positionalData = value;
            }
        }

        public bool AdvancedSettings
        {
            get
            {
                return _advancedSettings;
            }
            set
            {
                _advancedSettings = value;
            }
        }

        public ICommand AdvancedSettingsToggle
        {
            get { return _toggleAdvancedSettings ?? (_toggleAdvancedSettings = new DelegateCommand(ToggleAdvancedSettings)); }
        }

        public ICommand SelectedGroundChanged
        {
            get { return _selectedGroundChanged ?? (_selectedGroundChanged = new DelegateCommand(SelectedGroundChange)); }
        }

        private void SelectedGroundChange()
        {
            OnPropertyChanged("Ground");
        }

        private void ToggleAdvancedSettings()
        {
            AdvancedSettings = !AdvancedSettings;
            OnPropertyChanged("AdvancedSettings");
        }

        public IEnumerable<GroundType> GroundTypes
        {
            get { return Enum.GetValues(typeof(GroundType)).Cast<GroundType>(); }
        }

        public GameSetupViewModel(){}

        public GameSetupViewModel(IGroundReader reader)
        {
            _advancedSettings = false;
            Ground = new Ground();

            var streams = Directory.GetFiles("Grounds", "*.json").Select(f => new StreamReader(f).BaseStream).ToList();

            _selectableGrounds = new ObservableCollection<Ground>();
            
            foreach(var g in reader.ReadGrounds(streams))
            {
                _selectableGrounds.Add(g);
            }
        }
    }
}
