using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadiantTulip.View
{
    public class GameControl : ContentControl, INotifyPropertyChanged
    {
        public readonly static DependencyProperty GameProperty = DependencyProperty.Register("Game",  
            typeof(RadiantTulip.Model.Game), 
            typeof(GameControl), 
            new PropertyMetadata(new PropertyChangedCallback(Update)));
        public event PropertyChangedEventHandler PropertyChanged;

        private Canvas _canvas;
        private Table _table;
        private Model.Game _game;
        private IGameDrawer _drawer;

        [BindableAttribute(true)]
        public Model.Game Game
        {
            get
            {
                return (Model.Game)GetValue(GameProperty);
            }

            set
            {
                SetValue(GameProperty, value);
            }
        }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private static void Update(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var g = sender as GameControl;
            if (g != null)
            {
                g.OnGameChanged(g.Game);
            }
        }

        private void OnGameChanged(Model.Game game)
        {
            _drawer.DrawGame(_canvas, _table, game);
        }

        static GameControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GameControl), new FrameworkPropertyMetadata(typeof(GameControl)));
        }

        public GameControl()
        {
            _canvas = new Canvas();
            _canvas.Width = 400;
            _canvas.Height = 350;
            AddChild(_canvas);
            var reader = new FlowDocumentReader();
            var document = new FlowDocument();
            reader.Document = document;
            _table = new Table();
            _table.RowGroups.Add(new TableRowGroup());
            document.Blocks.Add(_table);

            var groundDrawer = new GroundDrawer();
            var playerDrawer = new PlayerDrawer();

            _drawer = new GameDrawer(groundDrawer, playerDrawer);
        }
    }
}
