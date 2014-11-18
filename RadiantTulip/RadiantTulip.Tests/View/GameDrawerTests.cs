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
using System.Windows.Documents;

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
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible = true }
                }
            });

            playerDrawer.Setup(p => p.Draw(It.IsAny<Player>(), canvas));

            drawer.DrawGame(canvas, table, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Two_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var table = new Table();
            var player1 = new Player { Visible = true };
            var player2 = new Player { Visible = true };
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            });

            playerDrawer.Setup(p => p.Draw(It.IsAny<Player>(), canvas));

            drawer.DrawGame(canvas, table, game);

            playerDrawer.Verify(p => p.Draw(player1, canvas), Times.Once);
            playerDrawer.Verify(p => p.Draw(player2, canvas), Times.Once);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false}
                }
            });

            drawer.DrawGame(canvas, table, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Never);
        }

        [Test]
        public void Doesnt_Draw_Non_Visible_Player_Valid_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() {Visible = false},
                    new Player() {Visible = true}
                }
            });

            drawer.DrawGame(canvas, table, game);

            playerDrawer.Verify(p => p.Draw(It.IsAny<Player>(), canvas), Times.Once);
        }

        [Test]
        public void Draws_Ground()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>() };
            game.Ground = new Ground();

            drawer.DrawGame(canvas, table, game);

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
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible = true }
                }
            });

            drawer.AddVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, table, game);

            visualArtifact.Verify(v => v.Draw(canvas, It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void Draws_Visual_Artifact_Multiple_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());

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
            drawer.DrawGame(canvas, table, game);

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
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible= true }
                }
            });

            visualArtifact1.Setup(v => v.Draw(canvas, It.IsAny<Player>()));
            visualArtifact2.Setup(v => v.Draw(canvas, It.IsAny<Player>()));

            drawer.AddVisualArtifact(visualArtifact1.Object);
            drawer.AddVisualArtifact(visualArtifact2.Object);
            drawer.DrawGame(canvas, table, game);

            visualArtifact1.Verify(v => v.Draw(canvas, It.IsAny<Player>()), Times.Once);
            visualArtifact2.Verify(v => v.Draw(canvas, It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void Calculates_Multiple_Descriptive_Artifacts()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact1 = new Mock<IDescriptiveArtifact>();
            var descriptiveArtifact2 = new Mock<IDescriptiveArtifact>();
            var player = new Player { Visible = true };

            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player
                }
            });

            descriptiveArtifact1.Setup(d => d.Calculate(new Player())).Returns(1);
            descriptiveArtifact1.Setup(d => d.GetName()).Returns("One");
            descriptiveArtifact2.Setup(d => d.Calculate(new Player())).Returns(2);
            descriptiveArtifact2.Setup(d => d.GetName()).Returns("Two");

            drawer.AddDescriptiveArtifact(descriptiveArtifact1.Object);
            drawer.AddDescriptiveArtifact(descriptiveArtifact2.Object);
            drawer.DrawGame(canvas, table, game);

            descriptiveArtifact1.Verify(d => d.Calculate(player), Times.Once);
            descriptiveArtifact2.Verify(d => d.Calculate(player), Times.Once);
            Assert.AreEqual("One", table.Columns[0].Name);
            Assert.AreEqual("Two", table.Columns[1].Name);
            Assert.NotNull(table.RowGroups[0].Rows[0].Cells[0]);
            Assert.NotNull(table.RowGroups[0].Rows[0].Cells[1]);
        }

        [Test]
        public void Calculates_One_Descriptive_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();
            var player = new Player { Visible = true };

            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    player
                }
            });

            descriptiveArtifact.Setup(d => d.Calculate(new Player())).Returns(1);
            descriptiveArtifact.Setup(d => d.GetName()).Returns("One");

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, table, game);

            descriptiveArtifact.Verify(d => d.Calculate(player), Times.Once);
            Assert.AreEqual("One", table.Columns[0].Name);
            Assert.NotNull(table.RowGroups[0].Rows[0].Cells[0]);
        }

        [Test]
        public void Draws_Descriptive_Artifact_Multiple_Players()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());

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

            descriptiveArtifact.Setup(d => d.Calculate(player1)).Returns(1);
            descriptiveArtifact.Setup(d => d.Calculate(player2)).Returns(2);
            descriptiveArtifact.Setup(d => d.GetName()).Returns("One");

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, table, game);

            descriptiveArtifact.Verify(d => d.Calculate(player1), Times.Once);
            descriptiveArtifact.Verify(d => d.Calculate(player2), Times.Once);
            Assert.AreEqual("One", table.Columns[0].Name);
            Assert.NotNull(table.RowGroups[0].Rows[0].Cells[0]);
            Assert.NotNull(table.RowGroups[0].Rows[1].Cells[0]);
        }

        [Test]
        public void Doesnt_Draw_Descriptive_Artifact_For_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var descriptiveArtifact = new Mock<IDescriptiveArtifact>();
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());

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

            descriptiveArtifact.Setup(d => d.Calculate(player1)).Returns(1);
            descriptiveArtifact.Setup(d => d.Calculate(player2)).Returns(2);
            descriptiveArtifact.Setup(d => d.GetName()).Returns("One");

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, table, game);

            descriptiveArtifact.Verify(d => d.Calculate(player1), Times.Never);
            descriptiveArtifact.Verify(d => d.Calculate(player2), Times.Once);

            Assert.AreEqual("One", table.Columns[0].Name);
            Assert.NotNull(table.RowGroups[0].Rows[0].Cells[0]);
        }

        [Test]
        public void Doesnt_Draw_Visual_Artifact_For_Non_Visible_Player()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());

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
            drawer.DrawGame(canvas, table, game);

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
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player() { Visible = true }
                }
            });

            descriptiveArtifact.Setup(d => d.Calculate(new Player())).Returns(1);

            drawer.AddDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, table, game);
            drawer.RemoveDescriptiveArtifact(descriptiveArtifact.Object);
            drawer.DrawGame(canvas, table, game);

            descriptiveArtifact.Verify(d => d.Calculate(It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void Remove_Visual_Artifact()
        {
            var playerDrawer = new Mock<IPlayerDrawer>();
            var groundDrawer = new Mock<IGroundDrawer>();
            var drawer = new GameDrawer(groundDrawer.Object, playerDrawer.Object);
            var visualArtifact = new Mock<IVisualArtifact>();
            var canvas = new Canvas();
            var table = new Table();
            table.RowGroups.Add(new TableRowGroup());
            var game = new Game() { Teams = new List<Team>(), Ground = new Ground() };
            game.Teams.Add(new Team
            {
                Players = new List<Player>()
                {
                    new Player { Visible =  true }
                }
            });

            drawer.AddVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, table, game);
            drawer.RemoveVisualArtifact(visualArtifact.Object);
            drawer.DrawGame(canvas, table, game);

            visualArtifact.Verify(v => v.Draw(canvas, It.IsAny<Player>()), Times.Once);
        }
    }
}
