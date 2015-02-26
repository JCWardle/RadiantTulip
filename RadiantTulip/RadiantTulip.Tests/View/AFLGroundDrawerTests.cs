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
            var ground = new Ground
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
        public void Straight_Ends_Adjust_For_Padding()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 1000
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var line1 = (Line)canvas.Children[3];
            AssertDiff(90.2, line1.X1);
            AssertDiff(942.4, line1.Y1);
            AssertDiff(90.2, line1.X2);
            AssertDiff(607.6, line1.Y2);
            var line2 = (Line)canvas.Children[4];
            AssertDiff(1759.7, line2.X1);
            AssertDiff(942.4, line2.Y1);
            AssertDiff(1759.7, line2.X2);
            AssertDiff(607.6, line2.Y2);
        }

        [Test]
        public void Centre_Square_Correct_Size()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
        public void Goal_Square_Adjusts_For_Padding()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 1000
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var goalSquare = (Rectangle)canvas.Children[5];
            AssertDiff(81.2, goalSquare.Width);
            AssertDiff(55.8, goalSquare.Height);
            AssertDiff(90.2, goalSquare.Margin.Left);
            AssertDiff(747.1, goalSquare.Margin.Top);
            goalSquare = (Rectangle)canvas.Children[6];
            AssertDiff(81.2, goalSquare.Width);
            AssertDiff(55.8, goalSquare.Height);
            AssertDiff(1678.5, goalSquare.Margin.Left);
            AssertDiff(747.1, goalSquare.Margin.Top);
        }

        [Test]
        public void Goal_Square_Correct_Positions()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
        public void Goal_Posts_Adjust_For_Padding()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 1000
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var goalpost = (Line)canvas.Children[9];
            AssertDiff(746.5, goalpost.Y1);
            AssertDiff(746.5, goalpost.Y2);
            AssertDiff(45.1, goalpost.X1);
            AssertDiff(90.2, goalpost.X2);
            goalpost = (Line)canvas.Children[10];
            AssertDiff(803.4, goalpost.Y1);
            AssertDiff(803.4, goalpost.Y2);
            AssertDiff(45.1, goalpost.X1);
            AssertDiff(90.2, goalpost.X2);
            goalpost = (Line)canvas.Children[13];
            AssertDiff(746.5, goalpost.Y1);
            AssertDiff(746.5, goalpost.Y2);
            AssertDiff(1804.8, goalpost.X1);
            AssertDiff(1759.7, goalpost.X2);
            goalpost = (Line)canvas.Children[14];
            AssertDiff(803.4, goalpost.Y1);
            AssertDiff(803.4, goalpost.Y2);
            AssertDiff(1804.8, goalpost.X1);
            AssertDiff(1759.7, goalpost.X2);
        }

        [Test]
        public void Point_Posts_Correct_Position()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
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
        public void Point_Posts_Adjust_For_Padding()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 1000
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var pointPost = (Line)canvas.Children[7];
            AssertDiff(689.7, pointPost.Y1);
            AssertDiff(689.7, pointPost.Y2);
            AssertDiff(63.1, pointPost.X1);
            AssertDiff(90.2, pointPost.X2);
            pointPost = (Line)canvas.Children[8];
            AssertDiff(860.2, pointPost.Y1);
            AssertDiff(860.2, pointPost.Y2);
            AssertDiff(63.1, pointPost.X1);
            AssertDiff(90.2, pointPost.X2);
            pointPost = (Line)canvas.Children[11];
            AssertDiff(689.7, pointPost.Y1);
            AssertDiff(689.7, pointPost.Y2);
            AssertDiff(1786.8, pointPost.X1);
            AssertDiff(1759.7, pointPost.X2);
            pointPost = (Line)canvas.Children[12];
            AssertDiff(860.2, pointPost.Y1);
            AssertDiff(860.2, pointPost.Y2);
            AssertDiff(1786.8, pointPost.X1);
            AssertDiff(1759.7, pointPost.X2);
        }

        [Test]
        public void All_Lines_Are_White()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            foreach(var c in canvas.Children)
            {
                var shape = (Shape)c;
                Assert.AreEqual(Brushes.White, shape.Stroke);
                Assert.AreEqual(null, shape.Fill);
            }
        }

        [Test]
        public void Boundy_Line_Adjusts_For_Padding()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 1000
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var boundry = (Ellipse)canvas.Children[15];
            AssertDiff(1669.51, boundry.Width);
            AssertDiff(1372.85, boundry.Height);
            AssertDiff(90.2, boundry.Margin.Left);
            AssertDiff(88.6, boundry.Margin.Top);
        }

        [Test]
        public void Boundy_Line_Position()
        {
            var ground = new Ground
            {
                Width = 18500,
                Height = 15500,
                Type = GroundType.AFL,
                Padding = 0
            };

            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };
            canvas.Measure(new System.Windows.Size(1850, 1550));
            canvas.Arrange(new Rect(0, 0, 1850, 1550));

            drawer.Draw(canvas);

            var boundry = (Ellipse)canvas.Children[15];
            Assert.AreEqual(1850, boundry.Width);
            Assert.AreEqual(1550, boundry.Height);
        }

        private void AssertDiff(double a, double b, double diff = 0.1)
        {
            Assert.That(Math.Abs(a - b), Is.LessThan(diff));
        }
    }
}
