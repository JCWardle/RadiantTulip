using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class AFLGroundDrawerTests
    {
        [Test]
        public void Correct_Width_And_Height()
        {
            var ground = new AFLGround
            {
                Width = 18500,
                Height = 15500
            };
            var drawer = new AFLGroundDrawer(ground);
            var canvas = new Canvas { Width = 1850, Height = 1550 };

            drawer.Draw(canvas);

            var mainGround = (Ellipse)canvas.Children[0];
            Assert.AreEqual(1850, mainGround.Width);
            Assert.AreEqual(1550, mainGround.Height);
        }

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

            drawer.Draw(canvas);

            var line1 = (Line)canvas.Children[0];
            Assert.AreEqual(1, line1.Width);
            Assert.AreEqual(192, line1.Height);
            Assert.AreEqual(861, line1.Margin.Top);
            Assert.AreEqual(0, line1.Margin.Left);
            var line2 = (Line)canvas.Children[1];
            Assert.AreEqual(1, line1.Width);
            Assert.AreEqual(192, line1.Height);
            Assert.AreEqual(861, line1.Margin.Top);
            Assert.AreEqual(1850, line1.Margin.Left);
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

            drawer.Draw(canvas);

            var centreSquare = (Rectangle)canvas.Children[0];
            Assert.AreEqual(50, centreSquare.Width);
            Assert.AreEqual(50, centreSquare.Height);      
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

            drawer.Draw(canvas);

            var centreSquare = (Rectangle)canvas.Children[0];
            Assert.AreEqual(675, centreSquare.Margin.Left);
            Assert.AreEqual(525, centreSquare.Margin.Top);
        }

        [Test]
        public void Fifty_Line_Correct_Position()
        {

        }

        [Test]
        public void Inner_Circle_Correct_Size()
        {

        }

        [Test]
        public void Centre_Circle_Correct_Size()
        {

        }

        [Test]
        public void Goal_Square_Correct_Size()
        {

        }

        [Test]
        public void Goal_Square_Correct_Positions()
        {

        }

        [Test]
        public void Goal_Posts_Correct_Distance_Appart()
        {

        }

        [Test] 
        public void Point_Posts_Correct_Distance_Appart()
        {

        }

        [Test]
        public void Goal_Posts_Correct_Position()
        {

        }

        [Test]
        public void Point_Posts_Correct_Position()
        {

        }
    }
}
