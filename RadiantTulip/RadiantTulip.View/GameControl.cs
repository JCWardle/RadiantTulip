using RadiantTulip.View.Game;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using RadiantTulip.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Media;

namespace RadiantTulip.View
{
    public class GameControl : ContentControl, INotifyPropertyChanged
    {
        private Canvas _canvas;
        private Table _table;
        private IGameDrawer _drawer;

        [Dependency]
        public IGameDrawer Drawer {
            get
            {
                return _drawer;
            }
            set
            {
                _drawer = value;
            }
        }

        public readonly static DependencyProperty SelectedPlayersProperty = DependencyProperty.Register("SelectedPlayers",
            typeof(List<Player>),
            typeof(GameControl));

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

        public readonly static DependencyProperty GameProperty = DependencyProperty.Register("Game",
            typeof(Model.Game),
            typeof(GameControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, GameChanged, null));

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

        private static void GameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var context = d as GameControl;
            context.Game = (Model.Game)e.NewValue;
        }

        public readonly static DependencyProperty CurretTimeProperty = DependencyProperty.Register("CurrentTime",
            typeof(TimeSpan),
            typeof(GameControl),
            new FrameworkPropertyMetadata(TimeSpan.MaxValue, FrameworkPropertyMetadataOptions.AffectsRender, TimeChanged, null));

        [BindableAttribute(true)]
        public TimeSpan CurrentTime
        {
            get
            {
                return (TimeSpan)GetValue(CurretTimeProperty);
            }

            set
            {
                SetValue(CurretTimeProperty, value);
            }
        }

        private static void TimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var context = d as GameControl;
            context.CurrentTime = (TimeSpan)e.NewValue;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (_drawer != null)
            {
                _drawer.DrawGame(_canvas, _table, Game);
            }
        }

        public GameControl()
        {
            _canvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(5)
            };
            AddChild(_canvas);
            var reader = new FlowDocumentReader();
            var document = new FlowDocument();
            reader.Document = document;
            _table = new Table();
            _table.RowGroups.Add(new TableRowGroup());
            document.Blocks.Add(_table);

            _canvas.MouseUp += Canvas_MouseUp;            
        }

        private void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.GetBindingExpression(GameControl.GameProperty).UpdateTarget();

            var element = _canvas.InputHitTest(Mouse.GetPosition(_canvas));
            if (element is FrameworkElement)
            {
                var shape = (FrameworkElement) element;

                foreach (var t in Game.Teams)
                {
                    var player = t.Players.FirstOrDefault(p => p.CurrentPosition.X == shape.Margin.Right && p.CurrentPosition.Y == shape.Margin.Top);
                    if (player != null)
                        SelectedPlayers.Add(player);
                }
            }
        }
    }
}
