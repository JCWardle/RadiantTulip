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
    public class WheelChairRugbyGroundDrawer : IGroundDrawer
    {
        private const int GOAL_LENGTH = 800;
        private const int KEY_AREA_LENGTH = 175;
        private const int KEY_AREA_HEIGHT = 800;
        private const int CENTRE_CIRCLE_DIAMETER = 360;
        private const int GROUND_LENGTH = 2800;
        private const int GROUND_HEIGHT = 1500;

        private Ground _ground;
        private double _scaleX;
        private double _scaleY;
        private Brush _color = Brushes.White;

        public WheelChairRugbyGroundDrawer(Ground ground)
        {
            _ground = ground;
        }
        
        public void Draw(Canvas canvas)
        {
            _scaleX = (_ground.Width - _ground.Padding * 2) / canvas.ActualWidth;
            _scaleY = (_ground.Height - _ground.Padding * 2) / canvas.ActualHeight;

            canvas.Children.Add(CentreCircle(canvas));
            canvas.Children.Add(Boundry(canvas));
            canvas.Children.Add(KeyArea(canvas, true));
            canvas.Children.Add(KeyArea(canvas, false));
        }

        private Shape KeyArea(Canvas canvas, bool left)
        {
            return new Rectangle
            {
                Stroke = _color,
                Width = ScaleWidth(KEY_AREA_LENGTH),
                Height = ScaleHeight(KEY_AREA_HEIGHT),
                Margin = new Thickness
                {
                    Top = ScaleHeight(_ground.Padding + (_ground.Height / 2) - KEY_AREA_HEIGHT / 2),
                    Left = left ? ScaleWidth(_ground.Padding) : ScaleWidth(_ground.Width - KEY_AREA_LENGTH)
                }
            };
        }

        private Shape CentreCircle(Canvas canvas)
        {
            var centreX = (canvas.ActualWidth / 2) - (ScaleWidth(CENTRE_CIRCLE_DIAMETER) / 2);
            var centreY = (canvas.ActualHeight / 2) - (ScaleHeight(CENTRE_CIRCLE_DIAMETER) / 2);

            return new Ellipse
            {
                Width = ScaleWidth(CENTRE_CIRCLE_DIAMETER),
                Height = ScaleHeight(CENTRE_CIRCLE_DIAMETER),
                Margin = new Thickness { Top = centreY, Left = centreX },
                Stroke = _color
            };
        }

        private Shape Boundry(Canvas canvas)
        {
            return new Rectangle
            {
                Width = ScaleWidth(GROUND_LENGTH - _ground.Padding * 2),
                Height = ScaleHeight(GROUND_HEIGHT - _ground.Padding * 2),
                Margin = new Thickness { Top = _ground.Padding, Left = _ground.Padding },
                Stroke = _color
            };
        }

        private double ScaleHeight(int value)
        {
            return value / _scaleY;
        }

        private double ScaleWidth(int value)
        {
            return value / _scaleX;
        }
    }
}
