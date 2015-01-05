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
            if (position.X > ground.Width || position.Y > ground.Height)
                return;

            var x = position.X / ground.Width * canvas.ActualWidth;
            var y = position.Y / ground.Height * canvas.ActualHeight;

            var circle = new Ellipse
            {
                Width = 5, 
                Height = 5, 
                Margin = new Thickness {Left = x, Top = y},
                Fill = new SolidColorBrush(Colors.Red)
            };
            canvas.Children.Add(circle);
        }
    }
}
