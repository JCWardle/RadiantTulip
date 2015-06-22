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

namespace RadiantTulip.View.Drawing
{
    public class WheelChairRugbyGroundDrawer : GroundDrawer
    {
        private const int GOAL_LENGTH = 800;
        private const int KEY_AREA_LENGTH = 175;
        private const int KEY_AREA_HEIGHT = 800;
        private const int CENTRE_CIRCLE_DIAMETER = 360;
        private const int GROUND_LENGTH = 2800;
        private const int GROUND_HEIGHT = 1500;

        private Brush _color = Brushes.White;

        public WheelChairRugbyGroundDrawer(Ground ground)
        {
            Ground = ground;
        }
        
        public override void Draw(Canvas canvas)
        {
            Setup(canvas);

            canvas.Children.Add(CentreCircle(canvas));
            canvas.Children.Add(Boundry(canvas));
            canvas.Children.Add(KeyArea(canvas, true));
            canvas.Children.Add(KeyArea(canvas, false));
            canvas.Children.Add(CentreLine(canvas));
        }

        private Shape CentreLine(Canvas canvas)
        {
            var result = new Line
             {
                 X1 = CentreX(),
                 X2 = CentreX(),
                 Y1 = 0,
                 Y2 = Ground.Height,
                 Stroke = _color
             };

            return ScaleLine(result);
        }

        private Shape KeyArea(Canvas canvas, bool left)
        {
            var result = new Rectangle
            {
                Stroke = _color,
                Width = KEY_AREA_LENGTH,
                Height = KEY_AREA_HEIGHT,
                Margin = new Thickness
                {
                    Top = CentreY() - (KEY_AREA_HEIGHT / 2),
                    Left = left ? 0 : Ground.Width - KEY_AREA_LENGTH
                }
            };

            return ScaleShape(result);
        }

        private Shape CentreCircle(Canvas canvas)
        {
            var centreX = CentreX() - (CENTRE_CIRCLE_DIAMETER / 2);
            var centreY = CentreY() - (CENTRE_CIRCLE_DIAMETER / 2);

            var result =  new Ellipse
            {
                Width = CENTRE_CIRCLE_DIAMETER,
                Height = CENTRE_CIRCLE_DIAMETER,
                Margin = new Thickness { Top = centreY, Left = centreX },
                Stroke = _color
            };

            return ScaleShape(result);
        }

        private Shape Boundry(Canvas canvas)
        {
            var result = new Rectangle
            {
                Width = GROUND_LENGTH,
                Height = GROUND_HEIGHT,
                Stroke = _color
            };
            
            return ScaleShape(result);
        }
    }
}
