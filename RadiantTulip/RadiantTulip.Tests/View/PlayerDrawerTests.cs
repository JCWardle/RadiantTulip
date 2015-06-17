using System;
using System.Windows;
using NUnit.Framework;
using RadiantTulip.Model;
using System.Windows.Controls;
using System.Windows.Shapes;
using RadiantTulip.View.Game;
using System.Windows.Media;
using PlayerSize = RadiantTulip.Model.Size;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class PlayerDrawerTests
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
        public void Convert_Player_Coordinates_Square_Ground()
        {
            var player = new Player
                {
                    Size = PlayerSize.Small,
                    CurrentPosition = new LinkedListNode<Position>(new Position
                    {
                        X = 500,
                        Y = 500
                    }),
                    Shape = PlayerShape.Circle
                };
            var ground = new Ground { Height = 1000, Width = 1000 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            canvas.Measure(new System.Windows.Size(150, 150));
            canvas.Arrange(new Rect(0, 0, 150, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse)canvas.Children[0];
            Assert.AreEqual(73.5, circle.Margin.Left);
            Assert.AreEqual(73.5, circle.Margin.Top);
            Assert.AreEqual(3, circle.Width);
            Assert.AreEqual(3, circle.Height);
        }

        [Test]
        public void Convert_Player_Coordinates_Rectangle_Ground()
        {
            var player = new Player
                {
                    Size = PlayerSize.Small,
                    CurrentPosition = new LinkedListNode<Position>(new Position
                    {
                        X = 250,
                        Y = 300
                    }),
                    Shape = PlayerShape.Circle
                };
            var ground = new Ground { Height = 500, Width = 1000 };
            var canvas = new Canvas { Width = 300, Height = 150 };
            canvas.Measure(new System.Windows.Size(300, 150));
            canvas.Arrange(new Rect(0, 0, 300, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse) canvas.Children[0];
            Assert.AreNotEqual(null, circle);
            Assert.AreEqual(72.0d, circle.Margin.Left);
            Assert.AreEqual(87.0d, circle.Margin.Top);
            Assert.AreEqual(6, circle.Width);
            Assert.AreEqual(6, circle.Height);
        }

        [Test]
        public void Player_Out_Of_Bounds_Left()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 500,
                    Y = 450
                })
            };
            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(0, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Ground_But_In_Padding_Left()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = -50,
                    Y = 200
                }),
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 400, Width = 200, Padding = 100};
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Bounds_Right()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = -50,
                    Y = 450
                })
            };
            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(0, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Ground_But_In_Padding_Right()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 250,
                    Y = 200
                }),
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 400, Width = 200, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Bounds_Top()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 100,
                    Y = -50
                })
            };
            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(0, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Ground_But_In_Padding_Top()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 100,
                    Y = -50
                }),
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 400, Width = 200, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Bounds_Bottom()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 100,
                    Y = 550
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(0, canvas.Children.Count);
        }

        [Test]
        public void Player_Out_Of_Ground_But_In_Padding_Bottom()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 100,
                    Y = 450
                }),
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 400, Width = 200, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Draws_With_Correct_Color()
        {
            var player = new Player
            {
                Size = PlayerSize.Medium,
                Colour = new Color { R = 0, G = 0, B = 255 },
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 10,
                    Y = 10
                }),
                Shape = PlayerShape.Circle
            };

            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse)canvas.Children[0];
            Assert.AreNotEqual(null, circle);
            var brush = (SolidColorBrush)circle.Fill;
            Assert.AreEqual(new Color { R = 0, G = 0, B = 255 }, brush.Color);
        }

        [Test]
        public void Player_Draws_With_Correct_Size()
        {
            var player = new Player
            {
                Size = PlayerSize.Large,
                Colour = new Color { R = 0, G = 0, B = 255 },
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 10,
                    Y = 10
                }),
                Shape = PlayerShape.Circle
            };

            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            canvas.Measure(new System.Windows.Size(150, 150));
            canvas.Arrange(new Rect(0, 0, 150, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var circle = (Ellipse)canvas.Children[0];
            Assert.AreNotEqual(null, circle);
            Assert.AreEqual(15, circle.Height);
            Assert.AreEqual(15, circle.Width);
        }

        [Test]
        public void Player_Draws_With_Rectangle_Shape()
        {
            var player = new Player
            {
                Size = PlayerSize.Large,
                Colour = new Color { R = 0, G = 0, B = 255 },
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 10,
                    Y = 10
                }),
                Shape = PlayerShape.Square
            };

            var ground = new Ground { Height = 500, Width = 400 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            canvas.Measure(new System.Windows.Size(150, 150));
            canvas.Arrange(new Rect(0, 0, 150, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);

            Assert.AreEqual(1, canvas.Children.Count);
            var rectangle = (Rectangle)canvas.Children[0];
            Assert.AreNotEqual(null, rectangle);
            Assert.AreEqual(15, rectangle.Height);
            Assert.AreEqual(15, rectangle.Width);
        }

        [Test]
        public void Player_Outside_Ground_But_In_Padding_Bottom()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 100,
                    Y = 550
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Outside_Ground_But_In_Padding_Left()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 10,
                    Y = 10
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Outside_Ground_But_In_Padding_Right()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 450,
                    Y = 10
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Outside_Ground_But_In_Padding_Top()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 300,
                    Y = 10
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);
            Assert.AreEqual(1, canvas.Children.Count);
        }

        [Test]
        public void Player_Size_Is_In_Centimetres_And_Scales()
        {
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 300,
                    Y = 10
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            canvas.Measure(new System.Windows.Size(150, 150));
            canvas.Arrange(new Rect(0, 0, 150, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, _scaleSettings);

            var ellipse = (Ellipse)canvas.Children[0];
            Assert.AreEqual(11.25d, ellipse.Width);
            Assert.AreEqual(11.25d, ellipse.Height);
        }

        [Test]
        public void Player_Size_Scales_With_Scale_Settings()
        {
            var settings = new ReadOnlyDictionary<PlayerSize, int>(
            new Dictionary<PlayerSize, int>            
            {
                { PlayerSize.Small, 20 },
                { PlayerSize.Medium, 60 },
                { PlayerSize.Large, 70 },
                { PlayerSize.ExtraLarge, 80}
            });
            var player = new Player
            {
                CurrentPosition = new LinkedListNode<Position>(new Position
                {
                    X = 300,
                    Y = 10
                }),
                Shape = PlayerShape.Circle,
                Size = PlayerSize.Medium
            };
            var ground = new Ground { Height = 500, Width = 400, Padding = 100 };
            var canvas = new Canvas { Width = 150, Height = 150 };
            canvas.Measure(new System.Windows.Size(150, 150));
            canvas.Arrange(new Rect(0, 0, 150, 150));
            var drawer = new PlayerDrawer();

            drawer.Draw(player, ground, canvas, settings);

            var ellipse = (Ellipse)canvas.Children[0];
            Assert.AreEqual(22.50d, ellipse.Width);
            Assert.AreEqual(22.50d, ellipse.Height);
        }
    }
}
