using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Windows.Media;

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : BindableBase, IGameViewModel
    {
        private Model.Game _game;
        private readonly IModelUpdater _gameUpdater;
        private readonly DispatcherTimer _timer;
        private readonly TimeSpan _runTime;
        private ObservableCollection<Player> _selectedPlayers = new ObservableCollection<Player>();
        private ObservableCollection<Group> _groups = new ObservableCollection<Group>();

        private ICommand _play;
        private ICommand _pause;
        private ICommand _forward;
        private ICommand _rewind;
        private ICommand _stop;
        private ICommand _playerSelected;
        private ICommand _playerChecked;
        private ICommand _playerUnchecked;
        private ICommand _colourChanged;
        private ICommand _createGroup;

        public GameViewModel() {}


        public ICommand PlayCommand
        {
            get { return _play ?? (_play = new DelegateCommand(Play)); }
        }

        public ICommand PauseCommand
        {
            get { return _pause ?? (_pause = new DelegateCommand(Pause)); }
        }

        public ICommand ForwardCommand
        {
            get { return _forward ?? (_forward = new DelegateCommand(Forward)); }
        }

        public ICommand RewindCommand
        {
            get { return _rewind ?? (_rewind = new DelegateCommand(Rewind)); }
        }

        public ICommand StopCommand
        {
            get { return _stop ?? (_stop = new DelegateCommand(Stop)); }
        }

        public ICommand PlayerCheckedCommand
        {
            get { return _playerChecked ?? (_playerChecked = new DelegateCommand<Player>(PlayerChecked)); }
        }

        public ICommand PlayerSelectedCommand
        {
            get { return _playerSelected ?? (_playerSelected = new DelegateCommand<IList>(PlayerSelected)); }
        }

        public ICommand PlayerUncheckedCommand
        {
            get { return _playerUnchecked ?? (_playerUnchecked = new DelegateCommand<Player>(PlayerUnchecked)); }
        }

        public ICommand ColourChangedCommand
        {
            get { return _colourChanged ?? (_colourChanged = new DelegateCommand<object>(ColourChanged)); }
        }

        public ICommand CreateGroupCommand
        {
            get { return _createGroup ?? (_createGroup = new DelegateCommand<string>(CreateGroup)); }
        }

        private void CreateGroup(string name)
        {
            var players = new ObservableCollection<Player>();
            foreach(var p in SelectedPlayers)
                players.Add(p);

            var group = new Group
            {
                Players = players,
                Name = name
            };

            Groups.Add(group);
        }

        private void ColourChanged(object colour)
        {
            foreach (var p in SelectedPlayers)
                p.Colour = (Color)colour;
            UpdateView();
        }

        private void PlayerUnchecked(Player player)
        {
            SelectedPlayers.Remove(player);
        }

        private void PlayerChecked(Player player)
        {
            if (!_selectedPlayers.Contains(player))
                _selectedPlayers.Add(player);
        }

        private void PlayerSelected(IList players)
        {
            var collection = players.Cast<Player>();
            SelectedPlayers.Clear();
            
            foreach(var p in collection)
            {
                SelectedPlayers.Add(p);
            }
        }

        private void Stop()
        {
            _timer.Stop();
            _gameUpdater.Time = new TimeSpan(0, 0, 0, 0, 0);
            UpdateView();
        }

        private void Rewind()
        {
            var span = new TimeSpan(_gameUpdater.Increment.Ticks * 3);
            _timer.Interval = span;
            _timer.Start();
        }

        private void Forward()
        {
            var span = new TimeSpan(_gameUpdater.Increment.Ticks / 3);
            _timer.Interval = span;
            _timer.Start();
        }

        private void Pause()
        {
            _timer.Stop();
        }

        private void Play()
        {
            _timer.Interval = _gameUpdater.Increment;
            _timer.Start();
        }

        public Model.Game Game
        {
            get
            {
                return _game;
            }
        }

        public TimeSpan RunTime
        {
            get
            {
                return _runTime;
            }
        }

        public ObservableCollection<Player> SelectedPlayers
        {
            get
            {
                return _selectedPlayers;
            }

            set
            {
                _selectedPlayers = value;
            }
        }

        public ObservableCollection<Group> Groups
        {
            get
            {
                return _groups;
            }
        }

        public string CurrentTime
        {
            get
            {
                return string.Format("{0}:{1}", _gameUpdater.Time.Minutes, _gameUpdater.Time.Seconds);
            }
        }

        public Action UpdateView { get; set; }

        public GameViewModel(IUnityContainer container, IGameCreator creator)
        {
            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\SmallFullTeam.xlsx", FileMode.Open))
                _game = creator.CreateGame(stream);

            _gameUpdater = container.Resolve<IModelUpdater>(new ParameterOverride("game", _game));

            _timer = new DispatcherTimer();
            _timer.Tick += UpdateGame;
            _timer.Interval = _gameUpdater.Increment;
            _timer.Start();

            _runTime = _gameUpdater.MaxTime - _gameUpdater.Time;
        }

        private void UpdateGame(object o, EventArgs args)
        {
            _gameUpdater.Update();
            OnPropertyChanged("CurrentTime");
            CommandManager.InvalidateRequerySuggested();
            UpdateView();
        }
    }
}
