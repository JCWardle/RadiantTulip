using Moq;
using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using RadiantTulip.View.Game.VisualAffects;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class GameDrawerTests
    {
        [Test]
        public void Draws_One_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var affects = new List<IVisualAffect>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Ground = new Ground(), Teams = new List<Team>(), Ball = new Ball() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { 
                        Visible = true, 
                        CurrentPosition = new LinkedListNode<Position>(new Position
                            { 
                                X = 1, 
                                Y = 1, 
                                TimeStamp = TimeSpan.Zero 
                            })
                    }
                }
            });
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            playerDrawer.Setup(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas));

            drawer.DrawGame(canvas, game, new List<IVisualAffect>());

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Two_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var affects = new List<IVisualAffect>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var canvas = new Canvas();
            var player1 = new Player { Visible = true, CurrentPosition = new LinkedListNode<Position>(new Position { X = 1, Y = 1, TimeStamp = TimeSpan.Zero }) };
            var player2 = new Player { Visible = true, CurrentPosition = new LinkedListNode<Position>(new Position { X = 1, Y = 1, TimeStamp = TimeSpan.Zero }) };
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            });
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            playerDrawer.Setup(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas));

            drawer.DrawGame(canvas, game, new List<IVisualAffect>());

            playerDrawer.Verify(p => p.Draw(player1, It.IsAny<Ground>(), canvas), Times.Once);
            playerDrawer.Verify(p => p.Draw(player2, It.IsAny<Ground>(), canvas), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false}
                }
            });
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, new List<IVisualAffect>());

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas), Times.Never);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player_Valid_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var affects = new List<IVisualAffect>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false, CurrentPosition = new LinkedListNode<Position>(new Position { X = 1, Y = 1, TimeStamp = TimeSpan.Zero }) },
                    new Player() {Visible = true, CurrentPosition = new LinkedListNode<Position>(new Position { X = 1, Y = 1, TimeStamp = TimeSpan.Zero }) }
                }
            });
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Ground()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var affects = new List<IVisualAffect>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ball = new Ball() };
            game.Ground = new Ground();
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            groundDrawer.Verify(g => g.Draw(canvas), Times.Once);
        }

        [Test]
        public void Draws_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var affects = new List<IVisualAffect>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var visualArtifact = new Mock<IVisualAffect>();
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible = true }
                }
            });
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            affects.Add(visualArtifact.Object);

            drawer.DrawGame(canvas, game, affects);

            visualArtifact.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Draws_Multiple_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var affects = new List<IVisualAffect>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var visualArtifact1 = new Mock<IVisualAffect>();
            var visualArtifact2 = new Mock<IVisualAffect>();
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible= true }
                }
            });
            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            visualArtifact1.Setup(v => v.Draw(canvas));
            visualArtifact2.Setup(v => v.Draw(canvas));
            affects.Add(visualArtifact1.Object);
            affects.Add(visualArtifact2.Object);

            drawer.DrawGame(canvas, game, affects);

            visualArtifact1.Verify(v => v.Draw(canvas), Times.Once);
            visualArtifact2.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Clears_View_Before_New_Frame()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var affects = new List<IVisualAffect>();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            var canvas = new Canvas();
            
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible =  true }
                }
            });

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            canvas.Children.Add(new Ellipse());
            drawer.DrawGame(canvas, game, affects);

            Assert.AreEqual(0, canvas.Children.Count);
        }

        [Test]
        public void Creates_Ground_Drawer_From_Factory()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var ground = new Ground { Type = GroundType.AFL, Width = 20};

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, ground);

            groundFactory.Verify(g => g.CreateGroundDrawer(ground), Times.Once);
        }

        [Test]
        public void Draws_Ground_From_Ground_Drawer()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);            
            var affects = new List<IVisualAffect>();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            var canvas = new Canvas();
            groundDrawer.Setup(g => g.Draw(canvas));

            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible =  true }
                }
            });

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            groundDrawer.Verify(g => g.Draw(canvas), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Player_Without_Position()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var affects = new List<IVisualAffect>();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground(), Ball = new Ball() };
            var canvas = new Canvas();
            playerDrawer.Setup(g => g.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), It.IsAny<Canvas>()));

            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible =  true, CurrentPosition = null }
                }
            });

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            playerDrawer.Verify(g => g.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), It.IsAny<Canvas>()), Times.Never);
        }

        [Test]
        public void Draws_The_Ball()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var affects = new List<IVisualAffect>();
            var game = new Game()
            {
                Teams = new List<Team>(),
                Ground = new Ground(),
                Ball = new Ball()
                {
                    CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100, TimeStamp = TimeSpan.Zero })
                }
            };
            var canvas = new Canvas();
            ballDrawer.Setup(b => b.Draw(canvas, It.IsAny<Ball>(), null, It.IsAny<Ground>()));

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            ballDrawer.Verify(b => b.Draw(canvas, It.IsAny<Ball>(), null, It.IsAny<Ground>()), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_The_Ball_Without_A_Position()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var affects = new List<IVisualAffect>();
            var game = new Game()
            {
                Teams = new List<Team>(),
                Ground = new Ground(),
                Ball = new Ball()
                {
                    CurrentPosition = null
                }
            };
            var canvas = new Canvas();
            ballDrawer.Setup(b => b.Draw(canvas, It.IsAny<Ball>(), null, It.IsAny<Ground>()));

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            ballDrawer.Verify(b => b.Draw(canvas, It.IsAny<Ball>(), null, It.IsAny<Ground>()), Times.Never);
        }

        [Test]
        public void Draws_The_Ball_With_A_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var affects = new List<IVisualAffect>();
            var game = new Game()
            {
                Teams = new List<Team>()
                {
                    new Team() {
                        Players = new List<Player>()
                        {
                            new Player { CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100, TimeStamp = TimeSpan.Zero }) }
                        }
                    }
                },
                Ground = new Ground(),
                Ball = new Ball()
                {
                    CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100, TimeStamp = TimeSpan.Zero })
                }
            };
            var canvas = new Canvas();
            ballDrawer.Setup(b => b.Draw(canvas, game.Ball, game.Teams[0].Players[0], game.Ground));

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            ballDrawer.Verify(b => b.Draw(canvas, game.Ball, game.Teams[0].Players[0], game.Ground), Times.Once);
        }

        [Test]
        public void Draws_The_Ball_With_A_Player_From_Second_Team()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var ballDrawer = new Mock<IBallDrawer>();
            var groundDrawer = new Mock<GroundDrawer>();
            var groundFactory = new Mock<IGroundDrawerFactory>();
            groundFactory.Setup(g => g.CreateGroundDrawer(It.IsAny<Ground>())).Returns(groundDrawer.Object);
            var affects = new List<IVisualAffect>();
            var game = new Game()
            {
                Teams = new List<Team>()
                {
                    new Team() { Players = new List<Player>() },
                    new Team() {
                        Players = new List<Player>()
                        {
                            new Player { CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100, TimeStamp = TimeSpan.Zero }) }
                        }
                    }
                },
                Ground = new Ground(),
                Ball = new Ball()
                {
                    CurrentPosition = new LinkedListNode<Position>(new Position { X = 100, Y = 100, TimeStamp = TimeSpan.Zero })
                }
            };
            var canvas = new Canvas();
            ballDrawer.Setup(b => b.Draw(canvas, game.Ball, game.Teams[1].Players[0], game.Ground));

            var drawer = new GameDrawer(groundFactory.Object, playerDrawer.Object, ballDrawer.Object, game.Ground);

            drawer.DrawGame(canvas, game, affects);

            ballDrawer.Verify(b => b.Draw(canvas, game.Ball, game.Teams[1].Players[0], game.Ground), Times.Once);
        }
    }
}
