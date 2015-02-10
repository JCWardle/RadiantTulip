using Moq;
using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using RadiantTulip.View.Game.VisualAffects;
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
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Ground = new Ground(), Teams = new List<Team>() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible = true }
                }
            });

            playerDrawer.Setup(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas));

            //drawer.DrawGame(canvas, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Two_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var player1 = new Player { Visible = true };
            var player2 = new Player { Visible = true };
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            });

            playerDrawer.Setup(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas));

            //drawer.DrawGame(canvas, game);

            playerDrawer.Verify(p => p.Draw(player1, It.IsAny<Ground>(), canvas), Times.Once);
            playerDrawer.Verify(p => p.Draw(player2, It.IsAny<Ground>(), canvas), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false}
                }
            });

            //drawer.DrawGame(canvas, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas), Times.Never);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player_Valid_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false},
                    new Player() {Visible = true}
                }
            });

            //drawer.DrawGame(canvas, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), It.IsAny<Ground>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Ground()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>() };
            game.Ground = new Ground();

            //drawer.DrawGame(canvas, game);

            groundDrawer.Verify(g => g.Draw(canvas, game.Ground), Times.Once);
        }

        [Test]
        public void Draws_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualAffect>();
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible = true }
                }
            });

            //drawer.DrawGame(canvas, game);

            visualArtifact.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Draws_Visual_Artifact_Multiple_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualAffect>();
            var canvas = new Canvas();

            var player1 = new Player { Visible = true, CurrentPosition = new Position { X = 1, Y = 1 } };
            var player2 = new Player { Visible = true, CurrentPosition = new Position { X = 2, Y = 2 } };
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            });

            //drawer.DrawGame(canvas, game);

            visualArtifact.Verify(v => v.Draw(canvas), Times.Once);
            visualArtifact.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Draws_Multiple_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact1 = new Mock<IVisualAffect>();
            var visualArtifact2 = new Mock<IVisualAffect>();
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible= true }
                }
            });

            visualArtifact1.Setup(v => v.Draw(canvas));
            visualArtifact2.Setup(v => v.Draw(canvas));

            //drawer.DrawGame(canvas, game);

            visualArtifact1.Verify(v => v.Draw(canvas), Times.Once);
            visualArtifact2.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Visual_Artifact_For_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualAffect>();
            var canvas = new Canvas();

            var player1 = new Player { Visible = false, CurrentPosition = new Position { X = 1, Y = 1 } };
            var player2 = new Player { Visible = true, CurrentPosition = new Position { X = 2, Y = 2 } };
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            });

            //drawer.DrawGame(canvas, game);

            visualArtifact.Verify(v => v.Draw(canvas), Times.Never);
            visualArtifact.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Remove_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualAffect>();
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible =  true }
                }
            });

            //drawer.DrawGame(canvas, game);
            //drawer.DrawGame(canvas, game);

            visualArtifact.Verify(v => v.Draw(canvas), Times.Once);
        }

        [Test]
        public void Clears_View_Before_New_Frame()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible =  true }
                }
            });

            canvas.Children.Add(new Ellipse());
            //drawer.DrawGame(canvas, game);

            Assert.AreEqual(0, canvas.Children.Count);
        }
    }
}
