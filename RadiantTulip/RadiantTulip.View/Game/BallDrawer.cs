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
        private const int BALL_PADDING = 10;

        public void Draw(Canvas canvas, Ball ball, Player player, Ground ground, IReadOnlyDictionary<Model.Size, int> scaleSettings)
        {
            var size = player == null ? (double)RadiantTulip.Model.Size.Medium : (double)player.Size + BALL_PADDING;
            size = size / ground.Width * canvas.ActualWidth;
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
