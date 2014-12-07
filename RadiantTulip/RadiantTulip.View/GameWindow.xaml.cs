using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RadiantTulip
{
    public partial class GameWindow : Window
    {
        private Canvas _canvas;
        private IModelUpdater _updater;
        private Game _game;
        private IGameDrawer _drawer;
        private Table _table;

        public GameWindow()
        {
            _canvas = (Canvas)((StackPanel)this.FindName("Panel")).FindName("MainCanvas");
            _table = (Table)((FlowDocumentReader)((StackPanel)this.FindName("Panel")).Children[1]).Document.FindName("Table");

            var converter = new GPSConverter();
            var spatialReader = new ExcelReader();
            var creator = new GameCreator(converter, spatialReader);

            using (var stream = new FileStream(@"E:\Code\RadiantTulip\TestData\Raw.xlsx", FileMode.Open))
                _game = creator.CreateGame(stream);

            _updater = new ModelUpdater(_game);
            _updater.Update();

            _drawer = new GameDrawer(new GroundDrawer(), new PlayerDrawer());
        }

        private void Draw()
        {
            _drawer.DrawGame(_canvas, new Table(), _game);
        }
    }
}
