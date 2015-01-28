﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
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

namespace RadiantTulip.View
{
    public partial class GameWindow
    {
        private readonly IGameDrawer _drawer;
        private Table _table;
        private IGameViewModel _view;

        public GameWindow(IUnityContainer container)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<IGameViewModel>();
            _drawer = container.Resolve<IGameDrawer>();

            _table = new Table();
            _table.RowGroups.Add(new TableRowGroup());
            _view = (IGameViewModel)this.DataContext;
            _view.UpdateView = new Action(ReRender);
        }

        protected void ReRender()
        {
            _drawer.DrawGame(Game, _table, _view.Game);
        }

        private void Game_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = Game.InputHitTest(Mouse.GetPosition(Game));
            var game = _view.Game;

            if (element is Shape)
            {
                var shape = (FrameworkElement)element;
                var x = (game.Ground.Width * (shape.Margin.Left + shape.Width / 2)) / Game.ActualWidth;
                var y = (game.Ground.Height * (shape.Margin.Top + shape.Height / 2)) / Game.ActualHeight;

                foreach (var t in game.Teams)
                {
                    var player = t.Players.FirstOrDefault(p => Math.Round(p.CurrentPosition.X, 0) == Math.Round(x, 0) && Math.Round(p.CurrentPosition.Y, 0) == Math.Round(y, 0));
                    if(player != null)
                        _view.SelectedPlayers.Add(player);
                }
            }
        }
    }
}
