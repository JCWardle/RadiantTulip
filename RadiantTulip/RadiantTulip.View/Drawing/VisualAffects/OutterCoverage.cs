using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadiantTulip.View.Drawing.VisualAffects
{
    public class OutterCoverage : IVisualAffect
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

            var triangles = new List<Polygon>();

            foreach(var p in _players.Where(p => p.CurrentPosition != null))
                FillInConnections(p, canvas).ForEach(c => triangles.Add(c));

            triangles.ForEach(t => canvas.Children.Add(t));
        }

        private List<Polygon> FillInConnections(Player player, Canvas canvas)
        {
            var result = new List<Polygon>();
            for(var i = 0; i < _players.Count; i++)
            {
                if (_players[0] == player)
                    continue;

                if (i + 1 >= _players.Count)
                    result.Add(CreateTriangle(player, _players[0], _players[1], canvas));
                else if (i + 2 >= _players.Count)
                    result.Add(CreateTriangle(player, _players[i + 1], _players[0], canvas));
                else
                    result.Add(CreateTriangle(player, _players[i + 1], _players[i + 2], canvas));
            }

            return result;
        }

        private Polygon CreateTriangle(Player player, Player player2, Player player3, Canvas canvas)
        {
            var triangle = new Polygon {Fill = new SolidColorBrush(Color.FromArgb(100, 0, 0, 125))};

            var transform = player.CurrentPosition.Value.TransformToCanvas(_ground, canvas);

            triangle.Points.Add(transform);
            transform = player2.CurrentPosition.Value.TransformToCanvas(_ground, canvas);
            triangle.Points.Add(transform);
            transform = player3.CurrentPosition.Value.TransformToCanvas(_ground, canvas);
            triangle.Points.Add(transform);

            return triangle;
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