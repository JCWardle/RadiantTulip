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
    public class GameControl : ContentControl
    {
        public readonly static DependencyProperty GameProperty = DependencyProperty.Register("Game",  typeof(RadiantTulip.Model.Game), typeof(GameControl), new PropertyMetadata());

        private Canvas _canvas;
        private Table _table;
        private Model.Game _game;

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
            document.Blocks.Add(_table);
        }

        [BindableAttribute(true)]
        public Model.Game Game 
        {
            get 
            {
                return _game; 
            }

            set
            {
                value = _game;
            }
        }
    }
}
