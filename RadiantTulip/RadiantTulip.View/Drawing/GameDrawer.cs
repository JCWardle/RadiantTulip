﻿using RadiantTulip.Model;
using RadiantTulip.View.Drawing.VisualAffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using GameModel = RadiantTulip.Model.Game;

namespace RadiantTulip.View.Drawing
{
    public class GameDrawer : IGameDrawer
    {
        private GroundDrawer _groundDrawer;
        private IPlayerDrawer _playerDrawer;
        private IBallDrawer _ballDrawer;
        private IReadOnlyDictionary<Size, int> _scaleSettings;

        /// <summary>
        /// Creates a new instance of the GameDrawer class
        /// </summary>
        /// <param name="groundFactory">The ground factory to make the ground drawer</param>
        /// <param name="playerDrawer">The player drawer</param>
        /// <param name="ballDrawer">The ball drawer</param>
        /// <param name="ground">The ground to draw</param>
        /// <param name="scaleSettings">The current player size scaling settings</param>
        public GameDrawer(IGroundDrawerFactory groundFactory, 
            IPlayerDrawer playerDrawer, 
            IBallDrawer ballDrawer, 
            Ground ground,
            IReadOnlyDictionary<Size, int> scaleSettings)
        {
            _groundDrawer = groundFactory.CreateGroundDrawer(ground);
            _playerDrawer = playerDrawer;
            _ballDrawer = ballDrawer;
            _scaleSettings = scaleSettings;
        }

        public void DrawGame(Canvas canvas, GameModel game, IList<IVisualAffect> visualAffects)
        {
            canvas.Children.Clear();
            canvas.Background = new SolidColorBrush(Colors.Green);

            foreach (var v in visualAffects)
                v.Draw(canvas);

            _groundDrawer.Draw(canvas);

            if(game.Ball.CurrentPosition != null)
            {
                var players = new List<Player>().AsEnumerable();

                foreach(var team in game.Teams)
                    players = players.Union(team.Players);

                var player = players.FirstOrDefault(p => p.CurrentPosition != null &&
                    p.CurrentPosition.Value.X == game.Ball.CurrentPosition.Value.X &&
                    p.CurrentPosition.Value.Y == game.Ball.CurrentPosition.Value.Y);
                _ballDrawer.Draw(canvas, game.Ball, player, game.Ground, _scaleSettings);
            }

            foreach (var t in game.Teams)
            {
                foreach (var p in t.Players.Where(p => p.Visible && p.CurrentPosition != null))
                {
                    _playerDrawer.Draw(p, game.Ground, canvas, _scaleSettings);
                }
            }
        }
    }
}
