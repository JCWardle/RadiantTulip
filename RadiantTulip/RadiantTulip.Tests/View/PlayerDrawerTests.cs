
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
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas);

            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse)canvas.InputHitTest(new Point(75, 75));
            Assert.AreNotEqual(null, circle);
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
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();
            
            drawer.Draw(player, ground, canvas);
            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse)canvas.InputHitTest(new Point(37.5, 90));
            Assert.AreNotEqual(null, circle);
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
