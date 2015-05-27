using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using RadiantTulip.View.Game;
using System.Windows.Media;

namespace RadiantTulip.View.Game.VisualAffects
{
    public class Lines : IVisualAffect
    {
        private IList<Player> _players;
        private Ground _ground;

        public Lines(IList<Player> players, Ground ground)
        {
            _players = players;
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            Position previousPosition = null;
            foreach(var p in _players.Where(p => p.CurrentPosition != null))
            {
                if (previousPosition != null)
                {
                    var firstPos = previousPosition.TransformToCanvas(_ground, canvas);
                    var secondPos = p.CurrentPosition.Value.TransformToCanvas(_ground, canvas);
                    var line = new Line()
                    {
                        X1 = firstPos.X,
                        Y1 = firstPos.Y,
                        X2 = secondPos.X,
                        Y2 = secondPos.Y,
                        StrokeThickness = 2,
                        Stroke = Brushes.LightSteelBlue
                    };
                    canvas.Children.Add(line);
                }

                previousPosition = p.CurrentPosition.Value;
            }
        }

        public bool AffectFor(List<Model.Player> players, GroupAffect affect)
        {
            if (players.Count != _players.Count || affect != GroupAffect.Lines)
                return false;

            foreach(var p in players)
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
