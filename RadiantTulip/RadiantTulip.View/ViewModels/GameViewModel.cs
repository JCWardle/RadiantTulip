using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using System.Threading;

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : BindableBase, IGameViewModel
    {
        private Model.Game _game;
        private readonly IModelUpdater _gameUpdater;
        private readonly IUnityContainer _container;
        private DelegateCommand _update;
        private DelegateCommand _play;
        private DelegateCommand _stop;
        private Thread _runner;
        private object _lock = new object();

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
            _runner = new Thread(GameRunner);
            _runner.Start();
        }

        private void Stop()
        {
            lock (_lock)
            {
                _runner.Interrupt();
            }
        }

        private void GameRunner()
        {
            try
            {
                var previous = DateTime.MaxValue;
                while (previous != _gameUpdater.Time)
                {
                    lock (_lock)
                    {
                        previous = _gameUpdater.Time;
                        _gameUpdater.Update();
                        OnPropertyChanged("Game");
                    }
                }
            }
            catch (ThreadInterruptedException e)
            {
                return;
            }
        }
         
        public GameViewModel(IUnityContainer container, IGameCreator creator)
        {
            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\SmallRaw.xlsx", FileMode.Open))
                Game = creator.CreateGame(stream);

            _gameUpdater = container.Resolve<IModelUpdater>(new ParameterOverride("game", _game));
            Update();
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
    }
}
