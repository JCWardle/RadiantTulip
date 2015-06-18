using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.View.Drawing;
using RadiantTulip.View.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;
using System.Diagnostics;
using System.IO;
using RadiantTulip.View.Settings;

namespace RadiantTulip.View
{
    public partial class GameWindow
    {
        private const string SETTINGS_LOCATION = "GameTypeSettings/{0}.json";
        private readonly IGameDrawer _drawer;
        private IGameViewModel _view;
        private IReadOnlyDictionary<Model.Size, int> _settings;

        public GameWindow(IUnityContainer container, ISizeSettings sizeSettings,Model.Game game)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<IGameViewModel>(new ParameterOverride("game", game));

            using(var stream =  new FileStream(String.Format(SETTINGS_LOCATION, game.Ground.Type), FileMode.Open))
            {
                _settings = sizeSettings.ReadSizeSettings(stream);
            }
            
            _view = (IGameViewModel)this.DataContext;
            _view.UpdateView = new Action(ReRender);
            _drawer = container.Resolve<IGameDrawer>(new ParameterOverride("ground", _view.Game.Ground),
                new ParameterOverride("scaleSettings", _settings));
        }

        protected void ReRender()
        {
            _drawer.DrawGame(this.Game, _view.Game, _view.VisualAffects);
        }

        private void Game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = Game.InputHitTest(Mouse.GetPosition(Game));
            var game = _view.Game;
            var canvas = (Canvas)sender;

            if (element is Shape)
            {
                var shape = (FrameworkElement)element;

                var x = shape.Margin.Left;
                var y = shape.Margin.Top;

                foreach (var t in game.Teams)
                {
                    foreach(var p in t.Players.Where(p => p.CurrentPosition != null))
                    {
                        var size = (double)_settings[p.Size]/ (game.Ground.Width) * canvas.ActualWidth;
                        var currentPosition = p.CurrentPosition.Value.TransformToCanvas(game.Ground, canvas);
                        if (x > currentPosition.X - size && x < currentPosition.X + size
                            && y > currentPosition.Y - size && y < currentPosition.Y + size)
                            _view.SelectedPlayers.Add(p);
                    }
                }
            }
        }
    }
}
