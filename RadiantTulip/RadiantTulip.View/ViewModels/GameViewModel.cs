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
using RadiantTulip.View.Drawing;
using RadiantTulip.View.Drawing.VisualAffects;
using Xceed.Wpf.Toolkit;

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : BindableBase, IGameViewModel
    {
        private SelectionState _state;
        private Model.Game _game;
        private readonly IModelUpdater _gameUpdater;
        private readonly DispatcherTimer _timer;
        private readonly TimeSpan _runTime;
        private IAffectFactory _affectFactory;
        private ObservableCollection<Player> _selectedPlayers = new ObservableCollection<Player>();
        private ObservableCollection<Group> _groups = new ObservableCollection<Group>();
        private List<IVisualAffect> _visualAffects;
        private Group _selectedGroup;
        //TODO Speed Tuner
        private int _speedTuner = 1000;

        #region Commands
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
        private ICommand _groupSelected;
        private ICommand _sizeChanged;
        private ICommand _playerAffectCheckedCommand;
        private ICommand _groupAffectCheckedCommand;
        private ICommand _groupAffectUncheckedCommand;
        private ICommand _playerAffectUncheckedCommand;
        private ICommand _shapeChangedCommand;
        private ICommand _resizeCommand;
        private ICommand _selectionTabLoadedCommand;
        private ICommand _visibilityChangedCommand;

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

        public ICommand GroupSelectedCommand
        {
            get { return _groupSelected ?? (_groupSelected = new DelegateCommand<object>(GroupSelected)); }
        }

        public ICommand SizeChangedCommand
        {
            get { return _sizeChanged ?? (_sizeChanged = new DelegateCommand<object>(SizeChanged)); }
        }

        public ICommand PlayerAffectCheckedCommand
        {
            get { return _playerAffectCheckedCommand ?? (_playerAffectCheckedCommand = new DelegateCommand<object>(PlayerAffectChecked)); }
        }

        public ICommand GroupAffectCheckedCommand
        {
            get { return _groupAffectCheckedCommand ?? (_groupAffectCheckedCommand = new DelegateCommand<object>(GroupAffectChecked)); }
        }

        public ICommand PlayerAffectUncheckedCommand
        {
            get { return _playerAffectUncheckedCommand ?? (_playerAffectUncheckedCommand = new DelegateCommand<object>(PlayerAffectUnchecked)); }
        }

        public ICommand GroupAffectUncheckedCommand
        {
            get { return _groupAffectUncheckedCommand ?? (_groupAffectUncheckedCommand = new DelegateCommand<object>(GroupAffectUnchecked)); }
        }

        public ICommand ShapeChangedCommand
        {
            get { return _shapeChangedCommand ?? (_shapeChangedCommand = new DelegateCommand<object>(ShapeChanged)); }
        }

        public ICommand ResizeCommand
        {
            get { return _resizeCommand ?? (_resizeCommand = new DelegateCommand(Resize)); }
        }

        public ICommand SelectionTabLoadedCommand
        {
            get { return _selectionTabLoadedCommand ?? (_selectionTabLoadedCommand = new DelegateCommand<Tuple<DataTemplate, DataTemplate, TabControl>>(SelectionTabLoaded)); }
        }
        
        public ICommand VisibilityChangedCommand
        {
            get { return _visibilityChangedCommand ?? (_visibilityChangedCommand = new DelegateCommand<bool?>(VisibilityChanged)); }
        }

        #endregion

        #region Window Command Implementations

        private void Resize()
        {
            UpdateView();
        }

        private void ShapeChanged(object obj)
        {
            var shape = (PlayerShape)obj;

            if (State == SelectionState.MultiplePlayers || State == SelectionState.SinglePlayer)
            {
                foreach (var p in SelectedPlayers)
                    p.Shape = shape;
            }
            else if (State == SelectionState.Group)
            {
                foreach (var p in SelectedGroup.Players)
                    p.Shape = shape;
            }
            UpdateView();
        }

        private void GroupAffectUnchecked(object obj)
        {
            var affect = (GroupAffect)obj;
            _visualAffects.RemoveAll(v => v.AffectFor(SelectedGroup.Players.ToList(), affect));
        }

        private void PlayerAffectUnchecked(object obj)
        {
            var affect = (PlayerAffect)obj;
            _visualAffects.RemoveAll(v => v.AffectFor(SelectedPlayers.ToList(), affect));
        }

        private void GroupAffectChecked(object obj)
        {
            var affect = (GroupAffect)obj;
            _visualAffects.Add(_affectFactory.CreateGroupEffect(SelectedGroup.Players.ToList(), affect, Game));
        }

        private void PlayerAffectChecked(object obj)
        {
            var affect = (PlayerAffect)obj;
            foreach(var p in SelectedPlayers)
                _visualAffects.Add(_affectFactory.CreatePlayerEffect(p, affect, Game));
        }

        private void GroupSelected(object group)
        {
            SelectedGroup = (Group)group;
            State = SelectionState.Group;
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

        private void ColourChanged(object picker)
        {
            var colour = ((ColorPicker)picker).SelectedColor;
            if (State == SelectionState.MultiplePlayers || State == SelectionState.SinglePlayer)
            {
                foreach (var p in SelectedPlayers)
                    p.Colour = colour;
            }
            else if (State == SelectionState.Group)
            {
                foreach (var p in SelectedGroup.Players)
                    p.Colour = colour;
            }
            UpdateView();
        }

        private void SizeChanged(object obj)
        {
            var size = (RadiantTulip.Model.Size)obj;
            if (State == SelectionState.MultiplePlayers || State == SelectionState.SinglePlayer)
            {
                foreach (var p in SelectedPlayers)
                    p.Size = size;
            }
            else if (State == SelectionState.Group)
            {
                foreach (var p in SelectedGroup.Players)
                    p.Size = size;
            }
            UpdateView();
        }

        private void VisibilityChanged(bool? visibility)
        {
            if (State == SelectionState.MultiplePlayers || State == SelectionState.SinglePlayer)
            {
                foreach (var p in SelectedPlayers)
                    p.Visible = visibility.Value;
            }
            else if (State == SelectionState.Group)
            {
                foreach (var p in SelectedGroup.Players)
                    p.Visible = visibility.Value;
            }
            UpdateView();
        }

        private void PlayerUnchecked(Player player)
        {
            SelectedGroup = null;
            SelectedPlayers.Remove(player);

            SetState();
        }

        private void SetState()
        {
            if (SelectedGroup != null)
                State = SelectionState.Group;
            else if (SelectedPlayers.Count == 1)
                State = SelectionState.SinglePlayer;
            else if (SelectedPlayers.Count > 1)
                State = SelectionState.MultiplePlayers;
            else
                State = SelectionState.None;
        }

        private void PlayerChecked(Player player)
        {
            SelectedGroup = null;
            if (!_selectedPlayers.Contains(player))
                _selectedPlayers.Add(player);

            SetState();
        }

        private void PlayerSelected(IList players)
        {
            SelectedGroup = null;
            var collection = players.Cast<Player>();
            SelectedPlayers.Clear();
            
            foreach(var p in collection)
            {
                SelectedPlayers.Add(p);
            }
            SetState();
            OnPropertyChanged("SelectedPlayers");
        }

        private void SelectionTabLoaded(Tuple<DataTemplate, DataTemplate, TabControl> data)
        {
            var tabControl = data.Item3;
            foreach(var t in _game.Teams)
            {
                var tabItem = new TabItem();
                tabItem.Header = t.Name;
                tabItem.ContentTemplate = data.Item1;
                tabItem.Content = t;
                tabControl.Items.Add(tabItem);
            }

            var groupItem = new TabItem();
            groupItem.Header = "Groups";
            groupItem.ContentTemplate = data.Item2;
            groupItem.Content = Groups;

            tabControl.Items.Add(groupItem);
        }

        #endregion

        #region Play Back Methods
        private void Stop()
        {
            _timer.Stop();
            _gameUpdater.Time = new TimeSpan(0, 0, 0, 0, 0);
            UpdateView();
            OnPropertyChanged("Playing");
        }

        private void Rewind()
        {
            if (_gameUpdater.Increment > TimeSpan.Zero)
                _gameUpdater.ChangeDirection();

            var span = new TimeSpan(_gameUpdater.Increment.Ticks / 3);
            _timer.Interval = -span;
            _timer.Start();
        }

        private void Forward()
        {
            if (_gameUpdater.Increment < TimeSpan.Zero)
                _gameUpdater.ChangeDirection();

            var span = new TimeSpan(_gameUpdater.Increment.Ticks / 3);
            _timer.Interval = span;
            _timer.Start();
        }

        private void Pause()
        {
            _timer.Stop();
            OnPropertyChanged("Playing");
        }

        private void Play()
        {
            if (_gameUpdater.Increment < TimeSpan.Zero)
                _gameUpdater.ChangeDirection();

            _timer.Interval = _gameUpdater.Increment;
            _timer.Start();
            OnPropertyChanged("Playing");
        }

        #endregion

        #region Binding Properties

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

        public SelectionState State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }

        public IEnumerable<RadiantTulip.Model.Size> SizeOptions
        {
            get
            {
                return Enum.GetValues(typeof(RadiantTulip.Model.Size)).Cast<RadiantTulip.Model.Size>();
            }
        }

        public IEnumerable<PlayerShape> ShapeOptions
        {
            get
            {
                return Enum.GetValues(typeof(PlayerShape)).Cast<PlayerShape>();
            }
        }

        public IEnumerable<GroupAffect> GroupAffects
        {
            get
            {
                return Enum.GetValues(typeof(GroupAffect)).Cast<GroupAffect>();
            }
        }

        public IEnumerable<PlayerAffect> PlayerAffects
        {
            get
            {
                return Enum.GetValues(typeof(PlayerAffect)).Cast<PlayerAffect>();
            }
        }

        public string CurrentTime
        {
            get
            {
                return _gameUpdater == null ? "" : string.Format("{0}:{1}.{2}", _gameUpdater.Time.Minutes, _gameUpdater.Time.Seconds, _gameUpdater.Time.Milliseconds);
            }
        }

        public List<IVisualAffect> VisualAffects
        {
            get
            {
                return _visualAffects;
            }
        }

        public Group SelectedGroup 
        { 
            get 
            {
                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value;
            }
        }

        public double CurrentTimeMilliseconds
        {
            get
            {
                return _gameUpdater == null ? 0d : _gameUpdater.Time.TotalMilliseconds;
            }
            set
            {
                var restart = _timer.IsEnabled;
                _timer.Stop();

                _gameUpdater.Time = TimeSpan.FromMilliseconds(value); ;

                if (restart)
                    _timer.Start();                
            }
        }

        public double FrameIncrement
        {
            get
            {
                return _gameUpdater == null ? 0d : _gameUpdater.Increment.TotalMilliseconds;
            }
        }

        public bool Playing
        {
            get 
            {
                return _timer == null ? true : _timer.IsEnabled;
            }
        }

        /// <summary>
        /// TODO Speed Tuner
        /// </summary>
        public TimeSpan SpeedTuner
        {
            get
            {
                return TimeSpan.FromMilliseconds(_speedTuner);
            }
        }

        /// <summary>
        /// TODO Speed Tuner
        /// </summary>
        public string SpeedTunerMilliseconds
        {
            set
            {
                _speedTuner = int.Parse(value);
                OnPropertyChanged("SpeedTuner");
            }
            get
            {
                return _speedTuner.ToString();
            }
        }

        #endregion

        public Action UpdateView { get; set; }

        public GameViewModel(IUnityContainer container, Model.Game game)
        {
            _game = game;

            _gameUpdater = container.Resolve<IModelUpdater>(new ParameterOverride("game", _game));
            _affectFactory = container.Resolve<IAffectFactory>();

            _timer = new DispatcherTimer();
            _timer.Tick += UpdateGame;
            _timer.Interval = _gameUpdater.Increment;
            _timer.Start();

            _runTime = _gameUpdater.MaxTime - _gameUpdater.Time;
            State = SelectionState.None;
            _visualAffects = new List<IVisualAffect>();
            OnPropertyChanged("Playing");

            //Speed Tuner
            _speedTuner = 10;
        }

        private void UpdateGame(object o, EventArgs args)
        {
            _gameUpdater.Update();
            OnPropertyChanged("CurrentTime");
            OnPropertyChanged("CurrentTimeMilliseconds");
            CommandManager.InvalidateRequerySuggested();
            UpdateView();
        }
    }
}
