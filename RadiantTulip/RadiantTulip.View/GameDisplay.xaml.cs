using Microsoft.Practices.Unity;
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
    public partial class GameDisplay : UserControl
    {
        private IGameDrawer _drawer;
        private Table _table;

        public readonly static DependencyProperty GameProperty = DependencyProperty.Register("Game",
            typeof(Model.Game),
            typeof(GameDisplay),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

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

        public readonly static DependencyProperty CurrentTimeProperty = DependencyProperty.Register("CurrentTime",
            typeof(TimeSpan),
            typeof(GameDisplay),
            new FrameworkPropertyMetadata(TimeSpan.MaxValue, FrameworkPropertyMetadataOptions.AffectsRender));

        [BindableAttribute(true)]
        public TimeSpan CurrentTime
        {
            get
            {
                return (TimeSpan)GetValue(CurrentTimeProperty);
            }

            set
            {
                SetValue(CurrentTimeProperty, value);
            }
        }

        [Dependency]
        public IGameDrawer Drawer
        {
            get
            {
                return _drawer;
            }
            set
            {
                _drawer = value;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            _drawer.DrawGame(Canvas, _table, Game);
        }

        public GameDisplay()
        {
            InitializeComponent();
            _table = new Table();
            _table.RowGroups.Add(new TableRowGroup());
        }
    }
}
