using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using System.Windows.Threading;
using System.Collections.Generic;

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : BindableBase, IGameViewModel
    {
        private Model.Game _game;
        private readonly IModelUpdater _gameUpdater;
        private DelegateCommand _update;
        private DelegateCommand _play;
        private DelegateCommand _stop;
        private readonly DispatcherTimer _timer;
        private TimeSpan _runTime;
        private List<Player> _players = new List<Player>(); 

        public GameViewModel() {}

        public ICommand UpdateCommand
        {
            get { return _update ?? (_update = new DelegateCommand(Update)); }
        }

        public ICommand PlayCommand
        {
            get { return _play ?? (_play = new DelegateCommand(Play)); }
        }

        public ICommand StopCommand
        {
            get { return _stop ?? (_stop = new DelegateCommand(Stop)); }
        }

        private void Update()
        {
            _gameUpdater.Update();
            OnPropertyChanged("Game");
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
            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\SmallRaw.xlsx", FileMode.Open))
                _game = creator.CreateGame(stream);

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

            set
            {
                var restart = _timer.IsEnabled;
                if(restart)
                    _timer.Stop();

                _gameUpdater.Time = value;

                if(restart)
                    _timer.Start();
            }
        }

        public List<Player> SelectedPlayers
        {
            get
            {
                return _players;
            }
        }
    }
}
