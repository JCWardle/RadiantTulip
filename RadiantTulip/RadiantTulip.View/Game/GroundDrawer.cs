using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RadiantTulip.View.Game
{
    public class GroundDrawer : IGroundDrawer
    {
        private double _previousCanvasHeight = 0;
        private double _previousCanvasWidth = 0;

        public void Draw(Canvas canvas, Model.Ground ground)
        {

            _previousCanvasHeight = canvas.Height;
            _previousCanvasWidth = canvas.Width;
            var imageSource = new BitmapImage(new Uri(ground.Image));
            canvas.Background = new ImageBrush(imageSource);
        }
    }
}
