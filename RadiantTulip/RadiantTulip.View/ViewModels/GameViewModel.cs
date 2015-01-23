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

        public List<Player> SelectedPlayers
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

        public string CurrentTime
        {
            get
            {
                return string.Format("{0}:{1}", _gameUpdater.Time.TotalMinutes, _gameUpdater.Time.Seconds);
            }
        }

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
        }

        private void Play()
        {
            _timer.Start();
        }

        private void Stop()
        {
            _timer.Stop();
        }
    }
}
