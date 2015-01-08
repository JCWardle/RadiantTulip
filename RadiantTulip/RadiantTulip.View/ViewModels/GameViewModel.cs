using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Windows.Threading;

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
        private DispatcherTimer _timer;

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
            OnPropertyChanged("Game");
        }
         
        public GameViewModel(IUnityContainer container, IGameCreator creator)
        {
            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\SmallRaw.xlsx", FileMode.Open))
                Game = creator.CreateGame(stream);

            _gameUpdater = container.Resolve<IModelUpdater>(new ParameterOverride("game", _game));
            Update();

            _timer = new DispatcherTimer();
            _timer.Tick += UpdateGame;
            _timer.Interval = _gameUpdater.Increment;
            _timer.Start();
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
