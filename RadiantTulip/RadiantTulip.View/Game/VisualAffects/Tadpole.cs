using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadiantTulip.View.Game.VisualAffects
{
    class Tadpole : Drawer, IVisualAffect
    {
        private Player _player;
        private Ground _ground;

        public Tadpole(Player player, Ground ground)
        {
            _player = player;
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            var currentPositionIndex = _player.Positions.IndexOf(_player.CurrentPosition);
            for(var i = 0; i < 4; i++)
            {
                if (currentPositionIndex - (i + 1) < 0)
                    break;

                var position = _player.Positions[currentPositionIndex - (i + 1)];
                var transform = TransformToCanvas(position.X, position.Y, _ground, canvas);

                var x = transform.Item1 - (double)_player.Size / 2;
                var y = transform.Item2 - (double)_player.Size / 2;
                var color = _player.Colour;
                color.A = 50;

                var circle = new Ellipse
                {
                    Width = (int)_player.Size,
                    Height = (int)_player.Size,
                    Margin = new Thickness { Left = x, Top = y },
                    Fill = new SolidColorBrush(color)
                };

                canvas.Children.Add(circle);
            }
        }


        public bool AffectFor(List<Player> players, GroupAffect affect)
        {
            return false;
        }

        public bool AffectFor(List<Player> players, PlayerAffect affect)
        {
            return players.Contains(_player) && affect == PlayerAffect.Tadpole;
        }
    }
}
