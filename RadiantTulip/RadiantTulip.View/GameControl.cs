using RadiantTulip.View.Game;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Practices.Unity;

namespace RadiantTulip.View
{
    public class GameControl : ContentControl
    {
        public readonly static DependencyProperty GameProperty = DependencyProperty.Register("Game",  
            typeof(Model.Game), 
            typeof(GameControl), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, Update));

        [Dependency]
        public IGameDrawer Drawer { get; set; }

        private Canvas _canvas;
        private Table _table;
        private Model.Game _game;
        

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

        private static object Update(DependencyObject sender, object e)
        {
            var g = sender as GameControl;
            if (e != null)
            {
                g.OnGameChanged((Model.Game)e);
            }
            return null;
        }

        private void OnGameChanged(Model.Game game)
        {
            if (Drawer != null)
            {
                Drawer.DrawGame(_canvas, _table, game);
            }
        }

        public GameControl()
        {
            _canvas = new Canvas { Width = 400, Height =  350};
            AddChild(_canvas);
            var reader = new FlowDocumentReader();
            var document = new FlowDocument();
            reader.Document = document;
            _table = new Table();
            _table.RowGroups.Add(new TableRowGroup());
            document.Blocks.Add(_table);
        }
    }
}
