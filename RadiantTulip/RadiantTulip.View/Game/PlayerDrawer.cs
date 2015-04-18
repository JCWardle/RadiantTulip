using System.Windows;
using RadiantTulip.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadiantTulip.View.Game
{
    /// <summary>
    /// Co-ordinates start at the top left at 0,0
    /// </summary>
    public class PlayerDrawer : IPlayerDrawer
    {
        public void Draw(Player player, Ground ground, Canvas canvas)
        {
            var position = player.CurrentPosition;
            if (position.X > ground.Width + ground.Padding || position.Y > ground.Height + ground.Padding
                || position.X < 0 || position.Y < 0)
                return;

            var transform = position.TransformToCanvas(ground, canvas);
            var x = transform.X - (double)player.Size / 2;
            var y = transform.Y - (double)player.Size / 2;

            Shape shape = null;

            if (player.Shape == PlayerShape.Circle)
                shape = new Ellipse();
            else if (player.Shape == PlayerShape.Square)
                shape = new Rectangle();

            shape.Width = (int)player.Size;
            shape.Height = (int)player.Size;
            shape.Margin = new Thickness {Left = x, Top = y};
            shape.Fill = new SolidColorBrush(player.Colour);
            canvas.Children.Add(shape);
        }
    }
}
