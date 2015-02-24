using RadiantTulip.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RadiantTulip.View.Game
{
    public class AFLGroundDrawer : IGroundDrawer
    {
        private const int CENTER_CIRCLE_DIAMETER = 1000;

        private Ground _ground;
        

        public AFLGroundDrawer(Ground ground)
        {
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            var scaleX = _ground.Width / canvas.ActualWidth;
            var scaleY = _ground.Height / canvas.ActualHeight;

            var centreCircle = new Ellipse
            {
                Width = CENTER_CIRCLE_DIAMETER / scaleX ,
                Height = CENTER_CIRCLE_DIAMETER / scaleY
            };
            centreCircle.Margin = new Thickness { Left = (canvas.ActualWidth / 2) - centreCircle.Width, Right = (canvas.ActualHeight / 2) - centreCircle.Height };

            canvas.Children.Add(centreCircle);
        }
    }
}
