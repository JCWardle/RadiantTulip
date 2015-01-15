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

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : BindableBase, IGameViewModel
    {
        private Model.Game _game;
        private readonly IModelUpdater _gameUpdater;
        private DelegateCommand _play;
        private DelegateCommand _stop;
        private readonly DispatcherTimer _timer;
        private readonly TimeSpan _runTime;
        private List<Player> _selectedPlayers = new List<Player>(); 

        public GameViewModel() {}

        public ICommand PlayCommand
        {
            get { return _play ?? (_play = new DelegateCommand(Play)); }
        }

        public ICommand StopCommand
        {
            get { return _stop ?? (_stop = new DelegateCommand(Stop)); }
        }

        private void Play()
        {
            _timer.Start();
        }

        private void Stop()
        {
            _timer.Stop();
        }

        private void UpdateGame(object o, EventArgs args)
        {
            _gameUpdater.Update();
            OnPropertyChanged("CurrentTime");
            OnPropertyChanged("Game");
            OnPropertyChanged("Players");
        }
         
        public GameViewModel(IUnityContainer container, IGameCreator creator)
        {
            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\SmallFullTeam.xlsx", FileMode.Open))
                Game = creator.CreateGame(stream);

            _gameUpdater = container.Resolve<IModelUpdater>(new ParameterOverride("game", _game));

            _timer = new DispatcherTimer();
            _timer.Tick += UpdateGame;
            _timer.Interval = _gameUpdater.Increment;
            _timer.Start();

            _runTime = _gameUpdater.MaxTime - _gameUpdater.Time;
        }

        public Model.Game Game
        {
            get
            {
                return _game;
            }

            set
            {
                _game = value;
            }
        }

        public TimeSpan RunTime 
        {
            get
            {
                return _runTime;
            }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                return _gameUpdater.Time;
            }
        }

        public double CurrentTimeMilliseconds
        {
            get
            {
                return _gameUpdater.Time.TotalMilliseconds;
            }

            set
            {
                var restart = _timer.IsEnabled;
                _timer.Stop();

                _gameUpdater.Time = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(value));

                if (restart)
                    _timer.Start();
            }
        }

        public List<Player> SelectedPlayers
        {
            get
            {
                return _selectedPlayers;
            }

            set
            {
                _selectedPlayers = value;
                OnPropertyChanged("SelectedPlayers");
            }
        }
    }
}
