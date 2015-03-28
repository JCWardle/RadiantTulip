using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadiantTulip.View.Game.VisualAffects
{
    public class OutterCoverage : Drawer, IVisualAffect
    {
        private IList<Model.Player> _players;
        private Model.Ground _ground;

        public OutterCoverage(IList<Player> players, Ground ground)
        {
            _players = players;
            _ground = ground;
        }
        public void Draw(Canvas canvas)
        {
            if (_players.Count < 3)
                return;

            var polygon = new Polyline { Fill = new SolidColorBrush(Color.FromArgb(50, 0, 0, 125)) };
            var points = new PointCollection();

            foreach(var p in _players)
            {
                AddPlayerConnections(p, canvas, points);
            }

            polygon.Points = new PointCollection();
            foreach (var p in points.Distinct())
                polygon.Points.Add(p);

            canvas.Children.Add(polygon);
        }

        private void AddPlayerConnections(Player player, Canvas canvas, PointCollection points)
        {
            var currentPlayerTransformation = TransformToCanvas(player.CurrentPosition.X, player.CurrentPosition.Y, _ground, canvas);
            var currentPlayerPoint = new Point { X = currentPlayerTransformation.Item1, Y = currentPlayerTransformation.Item2 };

            foreach(var p in _players)
            {
                if (p == player)
                    continue;
                var point = TransformToCanvas(p.CurrentPosition.X, p.CurrentPosition.Y, _ground, canvas);
                points.Add(new Point { X = point.Item1, Y = point.Item2 });
                points.Add(currentPlayerPoint);
            }
        }

        public bool AffectFor(List<Model.Player> players, GroupAffect affect)
        {
            if (affect != GroupAffect.OutterCoverage)
                return false;

            foreach (var p in players)
            {
                if (!_players.Contains(p))
                    return false;
            }

            return true;
        }

        public bool AffectFor(List<Model.Player> players, PlayerAffect affect)
        {
            return false;
        }
    }
}