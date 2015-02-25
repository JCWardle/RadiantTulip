using RadiantTulip.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace RadiantTulip.View.Game
{
    public class AFLGroundDrawer : IGroundDrawer
    {
        private const int CENTER_CIRCLE_DIAMETER = 1000;
        private const int INNER_CIRCLE_DIAMETER = 300;
        private const int CENTRE_SQUARE_LENGTH = 5000;
        private const int DISTANCE_BETWEEN_POSTS = 630;
        private const int GOAL_SQUARE_LENGTH = 900;
        private const int GOAL_POST_HEIGHT = 500;
        private const int BEHIND_POST_HEIGHT = 300;
        private const int FIFTY_DISTANCE_OUT = 5000;

        private Ground _ground;

        public AFLGroundDrawer(Ground ground)
        {
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            var scaleX = _ground.Width / canvas.ActualWidth;
            var scaleY = _ground.Height / canvas.ActualHeight;

            canvas.Children.Add(CreateCentreCircle(scaleX, scaleY, canvas, CENTER_CIRCLE_DIAMETER));
            canvas.Children.Add(CreateCentreCircle(scaleX, scaleY, canvas, INNER_CIRCLE_DIAMETER));

            canvas.Children.Add(CreateCentreSquare(scaleX, scaleY, canvas));

            canvas.Children.Add(CreateEnd(scaleY, canvas, 0));
            canvas.Children.Add(CreateEnd(scaleY, canvas, canvas.ActualWidth));

            canvas.Children.Add(CreateGoalSquare(scaleX, scaleY, canvas, true));
            canvas.Children.Add(CreateGoalSquare(scaleX, scaleY, canvas, false));

            var goalPosts = CreateGoalPosts(scaleX, canvas, true);
            goalPosts.AddRange(CreateGoalPosts(scaleX, canvas, false));
            goalPosts.ForEach(g => canvas.Children.Add(g));

            canvas.Children.Add(CreateBoundry(scaleX, scaleY, canvas));
        }
        private Shape CreateBoundry(double scaleX, double scaleY, Canvas canvas)
        {
            return new Ellipse
            {
                Width = _ground.Width / scaleX,
                Height = _ground.Height / scaleY,
                Margin = new Thickness(0, 0, 0, 0)
            };
        }

        private List<Shape> CreateGoalPosts(double scaleX, Canvas canvas, bool left)
        {
            var result = new List<Line>();

            //Behind Posts
            result.Add(new Line 
            { 
                Y1 = (canvas.ActualHeight / 2) - (DISTANCE_BETWEEN_POSTS / scaleX * 1.5),
                X1 = left ? - BEHIND_POST_HEIGHT / scaleX : canvas.ActualWidth + BEHIND_POST_HEIGHT / scaleX
            });
            result.Add(new Line 
            { 
                Y1 = (canvas.ActualHeight / 2) + (DISTANCE_BETWEEN_POSTS / scaleX * 1.5),
                X1 = left ? - BEHIND_POST_HEIGHT / scaleX : canvas.ActualWidth + BEHIND_POST_HEIGHT / scaleX
            });


            //Goal Posts
            result.Add(new Line 
            { 
                Y1 = (canvas.ActualHeight / 2) - (DISTANCE_BETWEEN_POSTS / scaleX / 2),
                X1 = left ? - GOAL_POST_HEIGHT / scaleX : canvas.ActualWidth + GOAL_POST_HEIGHT / scaleX
            });
            result.Add(new Line 
            { 
                Y1 = (canvas.ActualHeight / 2) + (DISTANCE_BETWEEN_POSTS / scaleX / 2),
                X1 = left ? - GOAL_POST_HEIGHT / scaleX : canvas.ActualWidth + GOAL_POST_HEIGHT / scaleX
            });

            foreach(var p in result)
            {
                p.Y2 = p.Y1;

                if (left)
                    p.X2 = 0;
                else
                    p.X2 = canvas.ActualWidth;
            }

            return result.Cast<Shape>().ToList();
        }

        private Rectangle CreateGoalSquare(double scaleX, double scaleY, Canvas canvas, bool left)
        {
            var result = new Rectangle
            {
                Width = Scale(scaleX, GOAL_SQUARE_LENGTH),
                Height = Scale(scaleY, DISTANCE_BETWEEN_POSTS)
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
                Y1 = (canvas.ActualHeight / 2) + Scale(scaleY, DISTANCE_BETWEEN_POSTS * 3),
                X2 = xPosition,
                Y2 = (canvas.ActualHeight / 2) - Scale(scaleY, DISTANCE_BETWEEN_POSTS * 3),
                StrokeThickness = 5
            };
        }

        private Shape CreateCentreSquare(double scaleX, double scaleY, Canvas canvas)
        {
            var result = new Rectangle
            {
                Width = Scale(scaleX, CENTRE_SQUARE_LENGTH),
                Height = Scale(scaleY, CENTRE_SQUARE_LENGTH)
            };

            result.Margin = CentrePosition(canvas, result.Width);
            return result;
        }

        private Shape CreateCentreCircle(double scaleX, double scaleY, Canvas canvas, int diameter)
        {
            var result = new Ellipse
            {
                Width = Scale(scaleX, diameter),
                Height = Scale(scaleY, diameter),
            };
            result.Margin = CentrePosition(canvas, result.Width);
            return result;
        }

        private Thickness CentrePosition(Canvas canvas, double diameter)
        {
            return new Thickness { Left = (canvas.ActualWidth / 2) - (diameter / 2), Top = (canvas.ActualHeight / 2) - (diameter / 2) };
        }

        private double Scale(double scale, int value)
        {
            return value / scale;
        }
    }
}
