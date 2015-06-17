using System.Windows;
using RadiantTulip.Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace RadiantTulip.View.Game
{
    /// <summary>
    /// Co-ordinates start at the top left at 0,0
    /// </summary>
    public class PlayerDrawer : IPlayerDrawer
    {
        private IReadOnlyDictionary<Model.Size, int> _scaleSettings;

        public PlayerDrawer(IReadOnlyDictionary<Model.Size, int> scaleSettings)
        {
            _scaleSettings = scaleSettings;
        }

        public void Draw(Player player, Ground ground, Canvas canvas)
        {
            var position = player.CurrentPosition;
            if (position.Value.X > ground.Width + ground.Padding || position.Value.Y > ground.Height + ground.Padding
                || position.Value.X + ground.Padding < 0 || position.Value.Y + ground.Padding < 0)
                return;

            var size = ((double)player.Size) / (ground.Width) * canvas.ActualWidth;

            var transform = position.Value.TransformToCanvas(ground, canvas);
            var x = transform.X - size / 2;
            var y = transform.Y - size / 2;

            Shape shape = null;

            if (player.Shape == PlayerShape.Circle)
                shape = new Ellipse();
            else if (player.Shape == PlayerShape.Square)
                shape = new Rectangle();

            shape.Width = size;
            shape.Height = size;
            shape.Margin = new Thickness {Left = x, Top = y};
            shape.Fill = new SolidColorBrush(player.Colour);
            canvas.Children.Add(shape);
        }
    }
}
