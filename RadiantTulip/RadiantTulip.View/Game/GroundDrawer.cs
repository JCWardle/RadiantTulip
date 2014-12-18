using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RadiantTulip.View.Game
{
    public class GroundDrawer : IGroundDrawer
    {
        public void Draw(Canvas canvas, Model.Ground ground)
        {
            var imageSource = new BitmapImage(new Uri(ground.Image));
            canvas.Background = new ImageBrush(imageSource);
        }
    }
}
