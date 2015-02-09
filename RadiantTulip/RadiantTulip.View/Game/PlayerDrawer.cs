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
    public class PlayerDrawer : Drawer, IPlayerDrawer
    {
        public void Draw(Player player, Ground ground, Canvas canvas)
        {
            var position = player.CurrentPosition;
            if (position.X > ground.Width || position.Y > ground.Height
                || position.X < 0 || position.Y < 0)
                return;

            var transform = TransformToCanvas(position.X, position.Y, ground, canvas);
            var x = transform.Item1 - (double)player.Size / 2;
            var y = transform.Item2 - (double)player.Size / 2;

            var circle = new Ellipse
            {
                Width = (int)player.Size, 
                Height = (int)player.Size, 
                Margin = new Thickness {Left = x, Top = y},
                Fill = new SolidColorBrush(player.Colour)
            };
            canvas.Children.Add(circle);
        }
    }
}
