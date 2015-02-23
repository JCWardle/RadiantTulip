using RadiantTulip.Model;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RadiantTulip.View.Game
{
    public class AFLGroundDrawer : IGroundDrawer
    {
        private Ground _ground;

        public AFLGroundDrawer(Ground ground)
        {
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            var imageSource = new BitmapImage(new Uri(_ground.Image));
            canvas.Background = new ImageBrush(imageSource);
        }
    }
}
