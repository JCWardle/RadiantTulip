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
    public class AFLGroundDrawer : GroundDrawer
    {
        private const int CENTER_CIRCLE_DIAMETER = 1000;
        private const int INNER_CIRCLE_DIAMETER = 300;
        private const int CENTRE_SQUARE_LENGTH = 5000;
        private const int DISTANCE_BETWEEN_POSTS = 630;
        private const int GOAL_SQUARE_LENGTH = 900;
        private const int GOAL_POST_HEIGHT = 500;
        private const int BEHIND_POST_HEIGHT = 300;
        private const int FIFTY_DISTANCE_OUT = 5000;

        private Brush _color = Brushes.White;

        public AFLGroundDrawer(Ground ground)
        {
            Ground = ground;
        }

        public override void Draw(Canvas canvas)
        {
            Setup(canvas);

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
            var xPositionCentre = left ? ScaleX(Ground.Padding) : canvas.ActualWidth - ScaleX(Ground.Padding);
            var yPositionCentre = canvas.ActualHeight / 2;

            var geometry = new StreamGeometry();

            using (var gc = geometry.Open())
            {
                gc.BeginFigure(new Point(xPositionCentre, yPositionCentre + ScaleY(FIFTY_DISTANCE_OUT)), false, false);

                gc.ArcTo(
                    point: new Point(xPositionCentre, yPositionCentre - ScaleY(FIFTY_DISTANCE_OUT)),
                    size: new System.Windows.Size(ScaleX(FIFTY_DISTANCE_OUT), ScaleY(FIFTY_DISTANCE_OUT)),
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

        private Shape CreateBoundry(Canvas canvas)
        {
            var result = new Ellipse
            {
                Width = Ground.Width,
                Height = Ground.Height,
                Stroke = _color
            };

            return ScaleShape(result);
        }

        private List<Shape> CreateGoalPosts(Canvas canvas, bool left)
        {
            var posts = new List<Line>();

            //Behind Posts
            posts.Add(new Line 
            { 
                Y1 = CentreY() - DISTANCE_BETWEEN_POSTS * 1.5,
                X1 = left ? -BEHIND_POST_HEIGHT : Ground.Width + BEHIND_POST_HEIGHT
            });
            posts.Add(new Line 
            {
                Y1 = CentreY() + DISTANCE_BETWEEN_POSTS * 1.5,
                X1 = left ? -BEHIND_POST_HEIGHT : Ground.Width + BEHIND_POST_HEIGHT
            });


            //Goal Posts
            posts.Add(new Line 
            {
                Y1 = CentreY() - DISTANCE_BETWEEN_POSTS / 2,
                X1 = left ? -GOAL_POST_HEIGHT : Ground.Width + GOAL_POST_HEIGHT
            });
            posts.Add(new Line 
            {
                Y1 = CentreY() + DISTANCE_BETWEEN_POSTS / 2,
                X1 = left ? -GOAL_POST_HEIGHT : Ground.Width + GOAL_POST_HEIGHT
            });

            var result = new List<Shape>();
            foreach(var p in posts)
            {
                p.Y2 = p.Y1;
                p.Stroke = _color;

                if (left)
                    p.X2 = 0;
                else
                    p.X2 = Ground.Width;

                result.Add(ScaleLine(p));
            }

            return result;
        }

        private Shape CreateGoalSquare(Canvas canvas, bool left)
        {
            var result = new Rectangle
            {
                Width = GOAL_SQUARE_LENGTH,
                Height = DISTANCE_BETWEEN_POSTS,
                Stroke = _color
            };

            var thickness = new Thickness { Top = CentreY() - (result.Height / 2) };

            if(!left)
                thickness.Left = Ground.Width - result.Width;

            result.Margin = thickness;
            return ScaleShape(result);
        }

        private Shape CreateEnd(Canvas canvas, Shape boundry, bool left)
        {
            var xPosition = left ? 0 : Ground.Width;

            var result = new Line
            {
                X1 = xPosition,
                Y1 = CentreY() + DISTANCE_BETWEEN_POSTS * 1.5,
                X2 = xPosition,
                Y2 = CentreY() - DISTANCE_BETWEEN_POSTS * 1.5,
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
            
            return ScaleLine(result);
        }

        private Shape CreateCentreSquare(Canvas canvas)
        {
            var result = new Rectangle
            {
                Width = CENTRE_SQUARE_LENGTH,
                Height = CENTRE_SQUARE_LENGTH,
                Stroke = _color
            };

            result.Margin = CentrePosition(canvas, result.Width, result.Height);
            return ScaleShape(result);
        }

        private Shape CreateCentreCircle(Canvas canvas, int diameter)
        {
            var result = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Stroke = _color
            };
            result.Margin = CentrePosition(canvas, result.Width, result.Height);
            return ScaleShape(result);
        }

        private Thickness CentrePosition(Canvas canvas, double width, double height)
        {
            return new Thickness 
            { 
                Left = CentreX() - (width / 2), 
                Top = CentreY() - (height / 2)
            };
        }
    }
}
