using Moq;
using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RadiantTulip.Tests.View
{
    [TestFixture]
    public class GameDrawerTests
    {
        [Test]
        public void Draws_One_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            drawer.DrawGame(canvas, grid, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Two_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player(),
                    new Player()
                }
            });

            drawer.DrawGame(canvas, grid, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Exactly(2));
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false}
                }
            });

            drawer.DrawGame(canvas, grid, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Never);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player_Valid_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false},
                    new Player() {Visible = true}
                }
            });

            drawer.DrawGame(canvas, grid, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Ground()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game();
            game.Ground = new Ground();

            drawer.DrawGame(canvas, grid, game);

            groundDrawer.Verify(g => g.Draw(canvas, game.Ground), Times.Once);
        }

        [Test]
        public void Draws_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            drawer.AddVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            visualArtifact.Verify(v => v.Draw(canvas, new Player()), Times.Once);
        }

        [Test]
        public void Draws_Visual_Artifact_Multiple_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();

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

            drawer.AddVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            visualArtifact.Verify(v => v.Draw(canvas, player1), Times.Once);
            visualArtifact.Verify(v => v.Draw(canvas, player2), Times.Once);
        }

        [Test]
        public void Draws_Multiple_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact1 = new Mock<IVisualArtifact>();
            var visualArtifact2 = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            drawer.AddVisualArtifact(visualArtifact1.Object);
            drawer.AddVisualArtifact(visualArtifact2.Object);
            drawer.DrawGame(canvas, grid, game);

            visualArtifact1.Verify(v => v.Draw(canvas, new Player()), Times.Once);
            visualArtifact2.Verify(v => v.Draw(canvas, new Player()), Times.Once);
        }

        [Test]
        public void Calculates_Multiple_Descriptive_Artifacts()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact1 = new Mock<IDescriptiveArtifact>();
            var descriptiveArtifact2 = new Mock<IDescriptiveArtifact>();

            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            descriptiveArtifact1.Setup(d => d.Calculate(new Player())).Returns(1);
            descriptiveArtifact2.Setup(d => d.Calculate(new Player())).Returns(2);

            drawer.AddDescriptiveArtifact(descriptiveArtifact1.Object);
            drawer.AddDescriptiveArtifact(descriptiveArtifact2.Object);
            drawer.DrawGame(canvas, grid, game);

            descriptiveArtifact1.Verify(d => d.Calculate(new Player()), Times.Once);
            descriptiveArtifact2.Verify(d => d.Calculate(new Player()), Times.Once);
        }

        [Test]
        public void Calculates_One_Descriptive_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();

            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            descriptiveArtifact.Setup(d => d.Calculate(new Player())).Returns(1);

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            descriptiveArtifact.Verify(d => d.Calculate(new Player()), Times.Once);
        }

        [Test]
        public void Draws_Descriptive_Artifact_Multiple_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();

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

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            descriptiveArtifact.Verify(d => d.Calculate(player1), Times.Once);
            descriptiveArtifact.Verify(d => d.Calculate(player2), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Descriptive_Artifact_For_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();

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

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            descriptiveArtifact.Verify(d => d.Calculate(player1), Times.Never);
            descriptiveArtifact.Verify(d => d.Calculate(player2), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Visual_Artifact_For_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();

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

            drawer.AddVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            visualArtifact.Verify(v => v.Draw(canvas, player1), Times.Never);
            visualArtifact.Verify(v => v.Draw(canvas, player2), Times.Once);
        }

        [Test]
        public void Remove_One_Descriptive_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();

            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            descriptiveArtifact.Setup(d => d.Calculate(new Player())).Returns(1);

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, grid, game);
            drawer.RemoveDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            descriptiveArtifact.Verify(d => d.Calculate(new Player()), Times.Once);
        }

        [Test]
        public void Remove_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var grid = new DataGrid();
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() 
                }
            });

            drawer.AddVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, grid, game);
            drawer.RemoveVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, grid, game);

            visualArtifact.Verify(v => v.Draw(canvas, new Player()), Times.Once);
        }
    }
}
