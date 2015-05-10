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
    [TestFixture]
    public class BallDrawerTests
    {
        [Test]
        public void Draws_Ball_Without_Player()
        {
            var canvas = new Canvas();
            var ballDrawer = new BallDrawer();
            var ball = new Ball { CurrentPosition = new Position { X = 100, Y = 100 }, Colour = Color.FromArgb(100, 255, 255, 0) };
            var ground = new Ground { Height = 500, Width = 500 };
            canvas.Measure(new System.Windows.Size(500, 500));
            canvas.Arrange(new Rect(0, 0, 500, 500));
            var drawer = new BallDrawer();

            drawer.Draw(canvas, ball, null, ground);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(100, shape.Margin.Left);
            Assert.AreEqual(100, shape.Margin.Top);
            var brush = (SolidColorBrush)shape.Fill;
            Assert.AreEqual(ball.Colour, brush.Color);
            Assert.AreEqual((int)RadiantTulip.Model.Size.Medium, shape.Width);
            Assert.AreEqual((int)RadiantTulip.Model.Size.Medium, shape.Height);
        }

        [Test]
        public void Draws_Ball_With_Player()
        {
            var canvas = new Canvas();
            var ballDrawer = new BallDrawer();
            var ball = new Ball { CurrentPosition = new Position { X = 100, Y = 100 }, Colour = Color.FromArgb(100, 255, 255, 0) };
            var ground = new Ground { Height = 500, Width = 500 };
            var player = new Player() { Size = RadiantTulip.Model.Size.ExtraLarge };
            canvas.Measure(new System.Windows.Size(500, 500));
            canvas.Arrange(new Rect(0, 0, 500, 500));
            var drawer = new BallDrawer();

            drawer.Draw(canvas, ball, player, ground);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(100, shape.Margin.Left);
            Assert.AreEqual(100, shape.Margin.Top);
            var brush = (SolidColorBrush)shape.Fill;
            Assert.AreEqual(ball.Colour, brush.Color);
            Assert.AreEqual((int)RadiantTulip.Model.Size.ExtraLarge + 2, shape.Width);
            Assert.AreEqual((int)RadiantTulip.Model.Size.ExtraLarge + 2, shape.Height);
        }

        [Test]
        public void Ball_Adjusts_For_Padding()
        {
            var canvas = new Canvas();
            var ballDrawer = new BallDrawer();
            var ball = new Ball { CurrentPosition = new Position { X = 100, Y = 100 }, Colour = Color.FromArgb(100, 255, 255, 0) };
            var ground = new Ground { Height = 500, Width = 500, Padding = 100 };
            canvas.Measure(new System.Windows.Size(700, 700));
            canvas.Arrange(new Rect(0, 0, 700, 700));
            var drawer = new BallDrawer();

            drawer.Draw(canvas, ball, null, ground);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(200, shape.Margin.Left);
            Assert.AreEqual(200, shape.Margin.Top);
        }
    }
}
