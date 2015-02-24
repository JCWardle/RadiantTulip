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
        private const int INNER_CIRCLE_DIAMETER = 300;
        private const int CENTRE_SQUARE_LENGTH = 5000;
        private const int DISTANCE_BETWEEN_POSTS = 630;
        private const int GOAL_SQUARE_LENGTH = 900;

        private Ground _ground;
        private Brush _color = Brushes.White;

        public AFLGroundDrawer(Ground ground)
        {
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            var scaleX = _ground.Width / canvas.ActualWidth;
            var scaleY = _ground.Height / canvas.ActualHeight;

            var centreCircle = CreateCentreCircle(scaleX, scaleY, canvas, CENTER_CIRCLE_DIAMETER);
            var innerCircle = CreateCentreCircle(scaleX, scaleY, canvas, INNER_CIRCLE_DIAMETER);

            var centreSquare = CreateCentreSquare(scaleX, scaleY, canvas);

            var leftEnd = CreateEnd(scaleY, canvas, 0);
            var rightEnd = CreateEnd(scaleY, canvas, canvas.ActualWidth);

            var leftEndGoalSquare = CreateGoalSquare(scaleX, scaleY, canvas, true);
            var rightEndGoalSquare = CreateGoalSquare(scaleX, scaleY, canvas, false);

            canvas.Children.Add(centreCircle);
            canvas.Children.Add(innerCircle);
            canvas.Children.Add(centreSquare);
            canvas.Children.Add(leftEnd);
            canvas.Children.Add(rightEnd);
            canvas.Children.Add(leftEndGoalSquare);
            canvas.Children.Add(rightEndGoalSquare);
        }

        private Rectangle CreateGoalSquare(double scaleX, double scaleY, Canvas canvas, bool left)
        {
            var result = new Rectangle
            {
                Width = GOAL_SQUARE_LENGTH / scaleX,
                Height = DISTANCE_BETWEEN_POSTS / scaleY,
                Stroke = _color
            };

            var thickness = new Thickness { Top = (canvas.ActualHeight / 2) - (result.Height / 2) };

            if (left)
                thickness.Left = 0;
            else
                thickness.Left = canvas.ActualWidth - result.Width;

            result.Margin = thickness;
            return result;
        }

        private Shape CreateEnd(double scaleY, Canvas canvas, double xPosition)
        {
            return new Line
            {
                X1 = xPosition,
                Y1 = (canvas.ActualHeight / 2) + (DISTANCE_BETWEEN_POSTS * 3) / scaleY,
                X2 = xPosition,
                Y2 = (canvas.ActualHeight / 2) - (DISTANCE_BETWEEN_POSTS * 3) / scaleY,
                StrokeThickness = 5,
                Stroke = _color
            };
        }

        private Shape CreateCentreSquare(double scaleX, double scaleY, Canvas canvas)
        {
            var result = new Rectangle
            {
                Width = CENTRE_SQUARE_LENGTH / scaleX,
                Height = CENTRE_SQUARE_LENGTH / scaleY,
                Stroke = _color
            };

            result.Margin = new Thickness { Left = (canvas.ActualWidth / 2) - (result.Width / 2), Top = (canvas.ActualHeight / 2) - (result.Height / 2) };
            return result;
        }

        private Shape CreateCentreCircle(double scaleX, double scaleY, Canvas canvas, int diameter)
        {
            var result = new Ellipse
            {
                Width = diameter / scaleX,
                Height = diameter / scaleY,
                Stroke = _color
            };
            result.Margin = new Thickness { Left = (canvas.ActualWidth / 2) - (result.Width / 2), Top = (canvas.ActualHeight / 2) - (result.Height / 2) };
            return result;
        }
    }
}
