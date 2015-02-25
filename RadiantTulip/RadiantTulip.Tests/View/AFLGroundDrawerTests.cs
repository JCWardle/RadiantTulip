using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class AFLGroundDrawerTests
    {
        [Test]
        public void Ground_Finishes_With_Straight_Ends()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500
            };
            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var line1 = (Line)canvas.Children[3];
            Assert.AreEqual(5, line1.StrokeThickness);
            Assert.AreEqual(0, line1.X1);
            Assert.AreEqual(964, line1.Y1);
            Assert.AreEqual(0, line1.X2);
            Assert.AreEqual(586, line1.Y2);
            var line2 = (Line)canvas.Children[4];
            Assert.AreEqual(5, line2.StrokeThickness);
            Assert.AreEqual(1850, line2.X1);
            Assert.AreEqual(964, line2.Y1);
            Assert.AreEqual(1850, line2.X2);
            Assert.AreEqual(586, line2.Y2);
        }

        [Test]
        public void Centre_Square_Correct_Size()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550};
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Rectangle)canvas.Children[2];
            Assert.AreEqual(500, centreSquare.Width);
            Assert.AreEqual(500, centreSquare.Height);      
        }

        [Test]
        public void Centre_Square_Correct_Position()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Rectangle)canvas.Children[2];
            Assert.AreEqual(675, centreSquare.Margin.Left);
            Assert.AreEqual(525, centreSquare.Margin.Top);
        }

        [Test]
        public void Inner_Circle_Correct_Size()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Ellipse)canvas.Children[1];
            Assert.AreEqual(30, centreSquare.Width);
            Assert.AreEqual(30, centreSquare.Height);
        }

        [Test]
        public void Inner_Circle_Correct_Position()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Ellipse)canvas.Children[1];
            Assert.AreEqual(910, centreSquare.Margin.Left);
            Assert.AreEqual(760, centreSquare.Margin.Top);
        }

        [Test]
        public void Centre_Circle_Correct_Size()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Ellipse)canvas.Children[0];
            Assert.AreEqual(100, centreSquare.Width);
            Assert.AreEqual(100, centreSquare.Height);
        }

        [Test]
        public void Centre_Circle_Correct_Position()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Ellipse)canvas.Children[0];
            Assert.AreEqual(875, centreSquare.Margin.Left);
            Assert.AreEqual(725, centreSquare.Margin.Top);
        }

        [Test]
        public void Goal_Square_Correct_Size()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Rectangle)canvas.Children[5];
            Assert.AreEqual(90, centreSquare.Width);
            Assert.AreEqual(63, centreSquare.Height);
            centreSquare = (Rectangle)canvas.Children[6];
            Assert.AreEqual(90, centreSquare.Width);
            Assert.AreEqual(63, centreSquare.Height);
        }

        [Test]
        public void Goal_Square_Correct_Positions()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var centreSquare = (Rectangle)canvas.Children[5];
            Assert.AreEqual(0, centreSquare.Margin.Left);
            Assert.AreEqual(743.5, centreSquare.Margin.Top);
            centreSquare = (Rectangle)canvas.Children[6];
            Assert.AreEqual(1760, centreSquare.Margin.Left);
            Assert.AreEqual(743.5, centreSquare.Margin.Top);
        }

        [Test]
        public void Goal_Posts_Correct_Position_And_Size()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var goalpost = (Line)canvas.Children[9];
            Assert.AreEqual(0, goalpost.X2);
            Assert.AreEqual(-50, goalpost.X1);
            Assert.AreEqual(743.5, goalpost.Y1);
            Assert.AreEqual(743.5, goalpost.Y2);
            goalpost = (Line)canvas.Children[10];
            Assert.AreEqual(0, goalpost.X2);
            Assert.AreEqual(-50, goalpost.X1);
            Assert.AreEqual(806.5, goalpost.Y1);
            Assert.AreEqual(806.5, goalpost.Y2);
            goalpost = (Line)canvas.Children[13];
            Assert.AreEqual(1850, goalpost.X2);
            Assert.AreEqual(1900, goalpost.X1);
            Assert.AreEqual(743.5, goalpost.Y1);
            Assert.AreEqual(743.5, goalpost.Y2);
            goalpost = (Line)canvas.Children[14];
            Assert.AreEqual(1850, goalpost.X2);
            Assert.AreEqual(1900, goalpost.X1);
            Assert.AreEqual(806.5, goalpost.Y1);
            Assert.AreEqual(806.5, goalpost.Y2);
        }

        [Test]
        public void Point_Posts_Correct_Position()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var goalpost = (Line)canvas.Children[7];
            Assert.AreEqual(0, goalpost.X2);
            Assert.AreEqual(-30, goalpost.X1);
            Assert.AreEqual(680.5, goalpost.Y1);
            Assert.AreEqual(680.5, goalpost.Y2);
            goalpost = (Line)canvas.Children[8];
            Assert.AreEqual(0, goalpost.X2);
            Assert.AreEqual(-30, goalpost.X1);
            Assert.AreEqual(869.5, goalpost.Y1);
            Assert.AreEqual(869.5, goalpost.Y2);
            goalpost = (Line)canvas.Children[11];
            Assert.AreEqual(1850, goalpost.X2);
            Assert.AreEqual(1880, goalpost.X1);
            Assert.AreEqual(680.5, goalpost.Y1);
            Assert.AreEqual(680.5, goalpost.Y2);
            goalpost = (Line)canvas.Children[12];
            Assert.AreEqual(1850, goalpost.X2);
            Assert.AreEqual(1880, goalpost.X1);
            Assert.AreEqual(869.5, goalpost.Y1);
            Assert.AreEqual(869.5, goalpost.Y2);
        }

        [Test]
        public void All_Lines_Are_White()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0,
                DistanceFrom50ToCentre = 150
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            foreach(var c in canvas.Children)
            {
                var shape = (Shape)c;
                shape.Stroke = Brushes.White;
                shape.Fill = null;
            }
        }
        
        [Test]
        public void Fifty_Line_Position()
        {

        }

        [Test]
        public void Boundy_Line_Position()
        {

        }
    }
}
