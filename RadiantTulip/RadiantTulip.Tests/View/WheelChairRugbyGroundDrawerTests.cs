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
using System.Windows.Shapes;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class WheelChairRugbyGroundDrawerTests
    {
        [Test]
        public void Centre_Circle_In_Middle()
        {
            var ground = new Ground
            {
                Width = 2800,
                Height = 1500,
                Type = GroundType.WheelChairRugby
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 2800, Height = 1500 };
            canvas.Measure(new System.Windows.Size(2800, 1500));
            canvas.Arrange(new Rect(0, 0, 2800, 1500));

            groundDrawer.Draw(canvas);

            var centreCircle = (Ellipse)canvas.Children[0];
            Assert.AreEqual(1220, centreCircle.Margin.Left);
            Assert.AreEqual(570, centreCircle.Margin.Top);
            Assert.AreEqual(360, centreCircle.Width);
            Assert.AreEqual(360, centreCircle.Height);
        }

        [Test]
        public void Centre_Circle_In_Middle_Adjusts_For_Scale()
        {
            var ground = new Ground
            {
                Width = 2800,
                Height = 1500,
                Type = GroundType.WheelChairRugby
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 5600, Height = 3000 };
            canvas.Measure(new System.Windows.Size(5600, 3000));
            canvas.Arrange(new Rect(0, 0, 5600, 3000));

            groundDrawer.Draw(canvas);

            var centreCircle = (Ellipse)canvas.Children[0];
            Assert.AreEqual(2440, centreCircle.Margin.Left);
            Assert.AreEqual(1140, centreCircle.Margin.Top);
            Assert.AreEqual(720, centreCircle.Width);
            Assert.AreEqual(720, centreCircle.Height);
        }

        [Test]
        public void Boundry_Is_Correct()
        {
            var ground = new Ground
            {
                Width = 2800,
                Height = 1500,
                Type = GroundType.WheelChairRugby
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 2800, Height = 1500 };
            canvas.Measure(new System.Windows.Size(2800, 1500));
            canvas.Arrange(new Rect(0, 0, 2800, 1500));

            groundDrawer.Draw(canvas);

            var boundry = (Rectangle)canvas.Children[1];
            Assert.AreEqual(0, boundry.Margin.Left);
            Assert.AreEqual(0, boundry.Margin.Top);
            Assert.AreEqual(2800, boundry.Width);
            Assert.AreEqual(1500, boundry.Height);
        }

        [Test]
        public void Boundry_Adjusts_For_Padding()
        {
            var ground = new Ground
            {
                Width = 3000,
                Height = 1700,
                Type = GroundType.WheelChairRugby,
                Padding = 100
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 3000, Height = 1700 };
            canvas.Measure(new System.Windows.Size(3000, 1700));
            canvas.Arrange(new Rect(0, 0, 3000, 1500));

            groundDrawer.Draw(canvas);

            var boundry = (Rectangle)canvas.Children[1];
            Assert.AreEqual(100, boundry.Margin.Left);
            Assert.AreEqual(100, boundry.Margin.Top);
            Assert.AreEqual(2785.7142857142858, boundry.Width);
            Assert.AreEqual(1473.3333333333335, boundry.Height);
        }

        [Test]
        public void Boundry_Scales()
        {
            var ground = new Ground
            {
                Width = 2800,
                Height = 1500,
                Type = GroundType.WheelChairRugby
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 1400, Height = 750 };
            canvas.Measure(new System.Windows.Size(1400, 750));
            canvas.Arrange(new Rect(0, 0, 1400, 750));

            groundDrawer.Draw(canvas);

            var boundry = (Rectangle)canvas.Children[1];
            Assert.AreEqual(0, boundry.Margin.Left);
            Assert.AreEqual(0, boundry.Margin.Top);
            Assert.AreEqual(1400, boundry.Width);
            Assert.AreEqual(750, boundry.Height);
        }
        
        [Test]
        public void Key_Areas_Correct()
        {
            var ground = new Ground
            {
                Width = 2800,
                Height = 1500,
                Type = GroundType.WheelChairRugby
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 2800, Height = 1500 };
            canvas.Measure(new System.Windows.Size(2800, 1500));
            canvas.Arrange(new Rect(0, 0, 2800, 1500));

            groundDrawer.Draw(canvas);

            var area1 = (Rectangle)canvas.Children[2];
            Assert.AreEqual(175, area1.Width);
            Assert.AreEqual(800, area1.Height);
            Assert.AreEqual(0, area1.Margin.Left);
            Assert.AreEqual(350, area1.Margin.Top);
            var area2 = (Rectangle)canvas.Children[3];
            Assert.AreEqual(175, area2.Width);
            Assert.AreEqual(800, area2.Height);
            Assert.AreEqual(2625, area2.Margin.Left);
            Assert.AreEqual(350, area2.Margin.Top);
        }

        [Test]
        public void Key_Areas_Adjust_For_Padding()
        {
            var ground = new Ground
            {
                Width = 3000,
                Height = 1700,
                Type = GroundType.WheelChairRugby,
                Padding = 100
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 3000, Height = 1700 };
            canvas.Measure(new System.Windows.Size(3000, 1700));
            canvas.Arrange(new Rect(0, 0, 3000, 1700));

            groundDrawer.Draw(canvas);

            var area1 = (Rectangle)canvas.Children[2];
            Assert.AreEqual(187.5, area1.Width);
            Assert.AreEqual(906.66666666666674, area1.Height);
            Assert.AreEqual(107.14285714285714, area1.Margin.Left);
            Assert.AreEqual(623.33333333333337, area1.Margin.Top);
            var area2 = (Rectangle)canvas.Children[3];
            Assert.AreEqual(187.5, area2.Width);
            Assert.AreEqual(906.66666666666674, area2.Height);
            Assert.AreEqual(3026.7857142857142, area2.Margin.Left);
            Assert.AreEqual(623.33333333333337, area2.Margin.Top);
        }

        [Test]
        public void Key_Areas_Scale()
        {
            var ground = new Ground
            {
                Width = 5600,
                Height = 3000,
                Type = GroundType.WheelChairRugby,
            };
            var groundDrawer = new WheelChairRugbyGroundDrawer(ground);
            var canvas = new Canvas { Width = 2300, Height = 1500 };
            canvas.Measure(new System.Windows.Size(2300, 1500));
            canvas.Arrange(new Rect(0, 0, 2300, 1500));

            groundDrawer.Draw(canvas);

            var area1 = (Rectangle)canvas.Children[2];
            Assert.AreEqual(71.875, area1.Width);
            Assert.AreEqual(400, area1.Height);
            Assert.AreEqual(0, area1.Margin.Left);
            Assert.AreEqual(550, area1.Margin.Top);
            var area2 = (Rectangle)canvas.Children[3];
            Assert.AreEqual(71.875, area2.Width);
            Assert.AreEqual(400, area2.Height);
            Assert.AreEqual(2228.125, area2.Margin.Left);
            Assert.AreEqual(550, area2.Margin.Top);
        }
    }
}
