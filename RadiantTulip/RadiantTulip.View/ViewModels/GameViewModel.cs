using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace RadiantTulip.View.ViewModels
{
    public class GameViewModel : RadiantTulip.View.BindableBase
    {
        private Model.Game _game;
        private IModelUpdater _gameUpdater;

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

        public GameViewModel()
        {
            var converter = new GPSConverter();
            var reader = new ExcelReader();
            var creator = new GameCreator(converter, reader);

            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\SmallRaw.xlsx", FileMode.Open))
                _game = creator.CreateGame(stream);

            _gameUpdater = new ModelUpdater(_game);
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
