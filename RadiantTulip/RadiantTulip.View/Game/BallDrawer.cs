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

namespace RadiantTulip.View.Game
{
    public class BallDrawer : IBallDrawer
    {
        public void Draw(Canvas canvas, Ball ball, Player player, Ground ground)
        {
            var size = player == null ? (int)RadiantTulip.Model.Size.Medium : (int)player.Size + 5;
            var position = ball.CurrentPosition.Value.TransformToCanvas(ground, canvas);

            canvas.Children.Add(new Ellipse
            {
                Height = size,
                Width = size,
                Margin = new Thickness
                {
                    Left = position.X - (double)size / 2,
                    Top = position.Y - (double)size / 2
                },
                Fill = new SolidColorBrush(ball.Colour)
            });
        }
    }
}
