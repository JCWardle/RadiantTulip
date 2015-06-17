using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PlayerSize = RadiantTulip.Model.Size;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class BallDrawerTests
    {
        private IReadOnlyDictionary<PlayerSize, int> _scaleSettings = new ReadOnlyDictionary<PlayerSize, int>(
        new Dictionary<PlayerSize, int>            
                {
                    { PlayerSize.Small, 20 },
                    { PlayerSize.Medium, 30 },
                    { PlayerSize.Large, 40},
                    { PlayerSize.ExtraLarge, 50}
                }
        );
        [Test]
        public void Draws_Ball_Without_Player()
        {
            var canvas = new Canvas();
            var ballDrawer = new BallDrawer();
            var ball = new Ball 
            { 
                CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100 }), 
                Colour = Color.FromArgb(100, 255, 255, 0) 
            };
            var ground = new Ground { Height = 500, Width = 500 };
            canvas.Measure(new System.Windows.Size(500, 500));
            canvas.Arrange(new Rect(0, 0, 500, 500));
            var drawer = new BallDrawer();

            drawer.Draw(canvas, ball, null, ground, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(85d, shape.Margin.Left);
            Assert.AreEqual(85d, shape.Margin.Top);
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
            var ball = new Ball
            {
                CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100 }),
                Colour = Color.FromArgb(100, 255, 255, 0)
            };
            var ground = new Ground { Height = 500, Width = 500 };
            var player = new Player() { Size = RadiantTulip.Model.Size.ExtraLarge };
            canvas.Measure(new System.Windows.Size(500, 500));
            canvas.Arrange(new Rect(0, 0, 500, 500));
            var drawer = new BallDrawer();

            drawer.Draw(canvas, ball, player, ground, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(70d, shape.Margin.Left);
            Assert.AreEqual(70d, shape.Margin.Top);
            var brush = (SolidColorBrush)shape.Fill;
            Assert.AreEqual(ball.Colour, brush.Color);
            Assert.AreEqual(60, shape.Width);
            Assert.AreEqual(60, shape.Height);
        }

        [Test]
        public void Ball_Adjusts_For_Padding()
        {
            var canvas = new Canvas();
            var ballDrawer = new BallDrawer();
            var ball = new Ball
            {
                CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100 }),
                Colour = Color.FromArgb(100, 255, 255, 0)
            };
            var ground = new Ground { Height = 500, Width = 500, Padding = 100 };
            canvas.Measure(new System.Windows.Size(700, 700));
            canvas.Arrange(new Rect(0, 0, 700, 700));
            var drawer = new BallDrawer();

            drawer.Draw(canvas, ball, null, ground, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(179d, shape.Margin.Left);
            Assert.AreEqual(179d, shape.Margin.Top);
        }

        [Test]
        public void Draws_Ball_With_Player_With_Scale()
        {
            var canvas = new Canvas();
            var ballDrawer = new BallDrawer();
            var ball = new Ball
            {
                CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100 }),
                Colour = Color.FromArgb(100, 255, 255, 0)
            };
            var ground = new Ground { Height = 500, Width = 500 };
            var player = new Player() { Size = RadiantTulip.Model.Size.ExtraLarge };
            canvas.Measure(new System.Windows.Size(500, 500));
            canvas.Arrange(new Rect(0, 0, 500, 500));
            var drawer = new BallDrawer();
            var settings = new ReadOnlyDictionary<PlayerSize, int>(
                new Dictionary<PlayerSize, int>            
                        {
                            { PlayerSize.Small, 20 },
                            { PlayerSize.Medium, 60 },
                            { PlayerSize.Large, 70},
                            { PlayerSize.ExtraLarge, 80}
                        }
                );

            drawer.Draw(canvas, ball, player, ground, settings);

            Assert.AreEqual(1, canvas.Children.Count);
            var shape = (Ellipse)canvas.Children[0];
            Assert.AreEqual(70d, shape.Margin.Left);
            Assert.AreEqual(70d, shape.Margin.Top);
            var brush = (SolidColorBrush)shape.Fill;
            Assert.AreEqual(ball.Colour, brush.Color);
            Assert.AreEqual(120, shape.Width);
            Assert.AreEqual(120, shape.Height);
        }
    }
}
