using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public static DependencyProperty SelectedPlayersProperty = DependencyProperty.Register("SelectedPlayers",
            typeof(List<Player>),
            typeof(GameDisplay),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [BindableAttribute(true)]
        public List<Player> SelectedPlayers
        {
            get
            {
                return (List<Player>)GetValue(SelectedPlayersProperty);
            }

            set
            {
                SetValue(SelectedPlayersProperty, value);
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
            Canvas.MouseUp += Canvas_MouseUp;
        }

        private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.GetBindingExpression(GameDisplay.GameProperty).UpdateTarget();

            var element = Canvas.InputHitTest(Mouse.GetPosition(Canvas));

            if (element is Shape)
            {
                var shape = (FrameworkElement)element;
                var x = Game.Ground.Width * shape.Margin.Left / Canvas.ActualWidth;
                var y = Game.Ground.Height * shape.Margin.Top / Canvas.ActualHeight;

                foreach (var t in Game.Teams)
                {
                    var player = t.Players.FirstOrDefault(p => Math.Round(p.CurrentPosition.X, 0) == Math.Round(x, 0) && Math.Round(p.CurrentPosition.Y, 0) == Math.Round(y, 0));

                    var list = new List<Player>();
                    list.Add(player);
                    if (SelectedPlayers != null)
                        list.AddRange(SelectedPlayers);
                    SelectedPlayers = list;
                }
            }
        }

        private double DistanceBetweenPoints(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }
    }
}
