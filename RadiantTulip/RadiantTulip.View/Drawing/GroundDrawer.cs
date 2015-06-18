using RadiantTulip.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RadiantTulip.View.Drawing
{
    public abstract class GroundDrawer
    {
        protected double XScale;
        protected double YScale;
        protected Ground Ground;

        public abstract void Draw(Canvas canvas);

        protected void Setup(Canvas canvas)
        {
            XScale = (Ground.Width + Ground.Padding * 2) / canvas.ActualWidth;
            YScale = (Ground.Height + Ground.Padding * 2) / canvas.ActualHeight;
        }

        protected Shape ScaleShape(Shape shape)
        {
            shape.Width = ScaleX(shape.Width);
            shape.Height = ScaleY(shape.Height);

            shape.Margin = new Thickness
            {
                Left = ScaleX(shape.Margin.Left) + ScaleX(Ground.Padding),
                Top = ScaleY(shape.Margin.Top) + ScaleY(Ground.Padding)
            };
            return shape;
        }

        protected Shape ScaleLine(Line line)
        {
            line.X1 = ScaleX(line.X1 + Ground.Padding);
            line.X2 = ScaleX(line.X2 + Ground.Padding);
            line.Y1 = ScaleY(line.Y1 + Ground.Padding);
            line.Y2 = ScaleY(line.Y2 + Ground.Padding);

            return line;
        }
        
        protected double CentreX()
        {
            return Ground.Width / 2;
        }

        protected double CentreY()
        {
            return Ground.Height / 2;
        }

        protected double ScaleX(double value)
        {
            return Scale(XScale, value);
        }

        protected double ScaleY(double value)
        {
            return Scale(YScale, value);
        }

        private double Scale(double scale, double value)
        {
            return value / scale;
        }
    }
}
