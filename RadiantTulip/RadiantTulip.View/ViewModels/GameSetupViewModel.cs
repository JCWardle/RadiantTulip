using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public class GameSetupViewModel : BindableBase, IGameSetupViewModel
    {
        private Ground _ground;
        private string _positionalData;
        private bool _advancedSettings;
        private IGameCreatorFactory _factory;
        private ICommand _toggleAdvancedSettings;
        private ICommand _selectedGroundChanged;
        private ICommand _startGame;
        private ObservableCollection<Ground> _selectableGrounds;
        private IUnityContainer _container;
        private Window _window;
        private Model.Game _game;

        public bool Loading { get; set; }
        public int LoadingProgress { get; set; }

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

        public IList<Ground> SelectableGrounds
        {
            get
            {
                return _selectableGrounds.Where(g => g.Type == _ground.Type).ToList();
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

        public ICommand StartGameCommand
        {
            get { return _startGame ?? (_startGame = new DelegateCommand<Window>(StartGame)); }
        }

        private void StartGame(Window window)
        {
            Loading = true;
            OnPropertyChanged("Loading");
            var worker = new BackgroundWorker();
            _window = window;
            worker.WorkerReportsProgress = true;
            worker.DoWork += CreateGame;
            worker.ProgressChanged += UpdateProgress;
            worker.RunWorkerCompleted += StartGameWindow;
            worker.RunWorkerAsync();
            
        }

        private void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            LoadingProgress = e.ProgressPercentage;
            OnPropertyChanged("LoadingProgress");
        }

        private void StartGameWindow(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_game == null)
            {
                Loading = false;
                LoadingProgress = 0;
                OnPropertyChanged("Loading");
                return;
            }

            var gameWindow = _container.Resolve<GameWindow>(new ParameterOverride("game", _game));
            gameWindow.Show();
            _window.Close();
        }

        private void CreateGame(object sender, DoWorkEventArgs e)
        {
            IGameCreator creator = null;
            try
            {
                creator = _factory.CreateGameCreator(_positionalData);
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("File type not supported, only txt, xlsx and xls are supported.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var reportProgress = new Action<int>((sender as BackgroundWorker).ReportProgress);

            using (var stream = new FileStream(_positionalData, FileMode.Open))
            {
                try
                {
                    _game = creator.CreateGame(stream, Ground, reportProgress);
                }
                catch (ArgumentException exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SelectedGroundChange()
        {
            OnPropertyChanged("Ground");
            OnPropertyChanged("SelectableGrounds");
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

        public GameSetupViewModel(IUnityContainer container, IGroundReader reader, IGameCreatorFactory factory)
        {
            _container = container;
            _factory = factory;
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
