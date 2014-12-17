using System.Windows;
using RadiantTulip.Model;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RadiantTulip.View.Game
{
    public class PlayerDrawer : IPlayerDrawer
    {
        public void Draw(Player player, Ground ground, Canvas canvas)
        {
            var position = player.CurrentPosition;
            if (position.X > ground.Width || position.Y > ground.Height)
                return;

            var x = position.X / ground.Width * canvas.Width;
            var y = position.Y / ground.Height * canvas.Height;

            var circle = new Ellipse { Width = 5, Height = 5 };
            circle.Margin  = new Thickness { Left = x, Top = y };
            canvas.Children.Add(circle);
        }
    }
}
