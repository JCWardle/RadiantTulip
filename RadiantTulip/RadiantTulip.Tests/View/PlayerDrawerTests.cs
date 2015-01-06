
using System;
using System.Windows;
using NUnit.Framework;
using RadiantTulip.Model;
using System.Windows.Controls;
using System.Windows.Shapes;
using RadiantTulip.View.Game;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class PlayerDrawerTests
    {
        [Test]
        public void Convert_Player_Coordinates_Square_Ground()
        {
            var player = new Player
                {
                    CurrentPosition = new Position
                    {
                        X = 500,
                        Y = 500
                    }
                };
            var ground = new Ground { Height = 1000, Width = 1000 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            canvas.Measure(new Size(150, 150));
            canvas.Arrange(new Rect(0, 0, 150, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas);

            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse)canvas.Children[0];
            Assert.AreEqual(75, circle.Margin.Left);
            Assert.AreEqual(75, circle.Margin.Top);
            Assert.AreEqual(5, circle.Width);
            Assert.AreEqual(5, circle.Height);
        }

        [Test]
        public void Convert_Player_Coordinates_Rectangle_Ground()
        {
            var player = new Player
                {
                    CurrentPosition = new Position
                    {
                        X = 250,
                        Y = 300
                    }
                };
            var ground = new Ground { Height = 500, Width = 1000 };
            var canvas = new Canvas { Width = 300, Height = 150 };
            canvas.Measure(new Size(300, 150));
            canvas.Arrange(new Rect(0, 0, 300, 150));
            var drawer = new PlayerDrawer();
            
            drawer.Draw(player, ground, canvas);
            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse) canvas.Children[0];
            Assert.AreNotEqual(null, circle);
            Assert.AreEqual(75, circle.Margin.Left);
            Assert.AreEqual(90, circle.Margin.Top);
            Assert.AreEqual(5, circle.Width);
            Assert.AreEqual(5, circle.Height);
        }

        [Test]
        public void Player_Out_Of_Bounds()
        {
            var player = new Player
            {
                CurrentPosition = new Position
                {
                    X = 500,
                    Y = 500
                }
            };
            var ground = new Ground { Height = 400, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas);
            Assert.AreEqual(0, canvas.Children.Count);
        }
    }
}
