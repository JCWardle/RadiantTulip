using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Unity;

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : BindableBase, IGameViewModel
    {
        private Model.Game _game;
        private readonly IModelUpdater _gameUpdater;
        private readonly IUnityContainer _container;

        public ICommand UpdateGame
        {
            get
            {
                return _command ??
                    (_command = new DelegateCommand(Update));
            }
        }

        private DelegateCommand _command;

        private void Update()
        {
            _gameUpdater.Update();
            OnPropertyChanged("Game");
        }

        public GameViewModel() { }
         
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
