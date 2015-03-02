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
        private Brush _color = Brushes.White;
        private double _scaleX;
        private double _scaleY;

        public AFLGroundDrawer(Ground ground)
        {
            _ground = ground;
        }

        public void Draw(Canvas canvas)
        {
            _scaleX = (_ground.Width + _ground.Padding * 2) / canvas.ActualWidth;
            _scaleY = (_ground.Height + _ground.Padding * 2) / canvas.ActualHeight;

            var boundry = CreateBoundry(canvas);

            canvas.Children.Add(CreateCentreCircle(canvas, CENTER_CIRCLE_DIAMETER));
            canvas.Children.Add(CreateCentreCircle(canvas, INNER_CIRCLE_DIAMETER));

            canvas.Children.Add(CreateCentreSquare(canvas));

            canvas.Children.Add(CreateEnd(canvas, boundry, true));
            canvas.Children.Add(CreateEnd(canvas, boundry, false));

            canvas.Children.Add(CreateGoalSquare(canvas, true));
            canvas.Children.Add(CreateGoalSquare(canvas, false));

            var goalPosts = CreateGoalPosts(canvas, true);
            goalPosts.AddRange(CreateGoalPosts(canvas, false));
            goalPosts.ForEach(g => canvas.Children.Add(g));

            canvas.Children.Add(boundry);
            canvas.Children.Add(Create50Line(canvas, true));
            canvas.Children.Add(Create50Line(canvas, false));
        }

        private Shape Create50Line(Canvas canvas, bool left)
        {
            var xPositionCentre = left ? ScaleWidth(_ground.Padding) : canvas.ActualWidth - ScaleWidth(_ground.Padding);
            var yPositionCentre = canvas.ActualHeight / 2;

            var geometry = new StreamGeometry();

            using (var gc = geometry.Open())
            {
                gc.BeginFigure(new Point(xPositionCentre, yPositionCentre + ScaleHeight(FIFTY_DISTANCE_OUT)), false, false);

                gc.ArcTo(
                    point: new Point(xPositionCentre, yPositionCentre - ScaleHeight(FIFTY_DISTANCE_OUT)),
                    size: new System.Windows.Size(ScaleWidth(FIFTY_DISTANCE_OUT), ScaleHeight(FIFTY_DISTANCE_OUT)),
                    rotationAngle: 180,
                    isLargeArc: true,
                    sweepDirection: left ? SweepDirection.Counterclockwise : SweepDirection.Clockwise,
                    isStroked: true,
                    isSmoothJoin: false);
            }

            return new Path
            {
                Stroke = _color,
                Data = geometry,
                StrokeThickness = 1
            };
        }

        private Ellipse CreateBoundry(Canvas canvas)
        {
            return new Ellipse
            {
                Width = ScaleWidth(_ground.Width),
                Height = ScaleHeight(_ground.Height),
                Margin = new Thickness(ScaleWidth(_ground.Padding), ScaleHeight(_ground.Padding), ScaleWidth(_ground.Padding), ScaleHeight(_ground.Padding)),
                Stroke = _color
            };
        }

        private List<Shape> CreateGoalPosts(Canvas canvas, bool left)
        {
            var result = new List<Line>();

            //Behind Posts
            result.Add(new Line 
            { 
                Y1 = (canvas.ActualHeight / 2) - (ScaleHeight(DISTANCE_BETWEEN_POSTS) * 1.5),
                X1 = left ? ScaleWidth(_ground.Padding) - ScaleWidth(BEHIND_POST_HEIGHT) : (canvas.ActualWidth - ScaleWidth(_ground.Padding)) + ScaleWidth(BEHIND_POST_HEIGHT)
            });
            result.Add(new Line 
            {
                Y1 = (canvas.ActualHeight / 2) + (ScaleHeight(DISTANCE_BETWEEN_POSTS) * 1.5),
                X1 = left ? ScaleWidth(_ground.Padding) - ScaleWidth(BEHIND_POST_HEIGHT) : (canvas.ActualWidth - ScaleWidth(_ground.Padding)) + ScaleWidth(BEHIND_POST_HEIGHT)
            });


            //Goal Posts
            result.Add(new Line 
            {
                Y1 = (canvas.ActualHeight / 2) - (ScaleHeight(DISTANCE_BETWEEN_POSTS) / 2),
                X1 = left ? ScaleWidth(_ground.Padding) - ScaleWidth(GOAL_POST_HEIGHT) : (canvas.ActualWidth - ScaleWidth(_ground.Padding)) + ScaleWidth(GOAL_POST_HEIGHT)
            });
            result.Add(new Line 
            {
                Y1 = (canvas.ActualHeight / 2) + (ScaleHeight(DISTANCE_BETWEEN_POSTS) / 2),
                X1 = left ? ScaleWidth(_ground.Padding) - ScaleWidth(GOAL_POST_HEIGHT) : (canvas.ActualWidth - ScaleWidth(_ground.Padding)) + ScaleWidth(GOAL_POST_HEIGHT)
            });

            foreach(var p in result)
            {
                p.Y2 = p.Y1;
                p.Stroke = _color;

                if (left)
                    p.X2 = 0 + ScaleWidth(_ground.Padding);
                else
                    p.X2 = canvas.ActualWidth - ScaleWidth(_ground.Padding);
            }

            return result.Cast<Shape>().ToList();
        }

        private Rectangle CreateGoalSquare(Canvas canvas, bool left)
        {
            var result = new Rectangle
            {
                Width = ScaleWidth(GOAL_SQUARE_LENGTH),
                Height = ScaleHeight(DISTANCE_BETWEEN_POSTS),
                Stroke = _color
            };

            var thickness = new Thickness { Top = (canvas.ActualHeight / 2) - (result.Height / 2) };

            if (left)
                thickness.Left = 0 + ScaleWidth(_ground.Padding);
            else
                thickness.Left = (canvas.ActualWidth - ScaleWidth(_ground.Padding)) - result.Width;

            result.Margin = thickness;
            return result;
        }

        private Shape CreateEnd(Canvas canvas, Ellipse boundry, bool left)
        {
            var xPosition = left ? 0 + ScaleWidth(_ground.Padding) : canvas.ActualWidth - ScaleWidth(_ground.Padding);

            var result = new Line
            {
                X1 = xPosition,
                Y1 = (canvas.ActualHeight / 2) + ScaleHeight(DISTANCE_BETWEEN_POSTS) * 1.5,
                X2 = xPosition,
                Y2 = (canvas.ActualHeight / 2) - ScaleHeight(DISTANCE_BETWEEN_POSTS) * 1.5,
                Stroke = _color
            };

            //TODO: Revisit putting the ends in the correct position on the ground
            /*var boundryCentreX = boundry.Margin.Left + (boundry.Width / 2);
            var boundryCentreY = boundry.Margin.Top + (boundry.Height / 2);

            var distanceIntoGround = Math.Pow(result.Y1 - boundryCentreY, 2);
            distanceIntoGround /= (boundry.Height / 2);
            distanceIntoGround = 1 - distanceIntoGround;
            distanceIntoGround *= (boundry.Width / 2);
            
            //Quadratic equation to solve for X
            var a = 1;
            var b = -boundryCentreX + -boundryCentreX;
            var c = (-boundryCentreX * -boundryCentreX) - distanceIntoGround;

            var discriminant = Math.Sqrt(Math.Pow(b, 2) - 4 * a * c);
            var solution = (-b + discriminant) / 2 * a;

            if (left)
            {
                result.X1 += distanceIntoGround;
                result.X2 += distanceIntoGround;
            }
            else
            {
                result.X1 -= distanceIntoGround;
                result.X2 -= distanceIntoGround;
            }*/
            
            return result;
        }

        private Shape CreateCentreSquare(Canvas canvas)
        {
            var result = new Rectangle
            {
                Width = ScaleWidth(CENTRE_SQUARE_LENGTH),
                Height = ScaleHeight(CENTRE_SQUARE_LENGTH),
                Stroke = _color
            };

            result.Margin = CentrePosition(canvas, result.Width, result.Height);
            return result;
        }

        private Shape CreateCentreCircle(Canvas canvas, int diameter)
        {
            var result = new Ellipse
            {
                Width = ScaleWidth(diameter),
                Height = ScaleHeight(diameter),
                Stroke = _color
            };
            result.Margin = CentrePosition(canvas, result.Width, result.Height);
            return result;
        }

        private Thickness CentrePosition(Canvas canvas, double width, double height)
        {
            return new Thickness 
            { 
                Left = (canvas.ActualWidth / 2) - (width / 2), 
                Top = (canvas.ActualHeight / 2) - (height / 2)
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
