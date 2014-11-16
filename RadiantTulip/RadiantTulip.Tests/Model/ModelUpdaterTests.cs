using NUnit.Framework;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class ModelUpdaterTests
    {
        [Test]
        public void Game_Getter()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            Assert.AreEqual(game, updater.Game);
        }

        [Test]
        public void Game_Starts_At_Start()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            Assert.AreEqual(new DateTime(2, 1, 1, 0, 0, 0, 0), updater.Time);
        }

        [Test]
        public void Player_Starts_At_First_Position()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            var currentPosition = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(1, currentPosition.X);
            Assert.AreEqual(1, currentPosition.Y);
        }

        [Test]
        public void Player_Moves_Next_Position_After_Update()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Update();

            var currentPosition = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(2, currentPosition.X);
            Assert.AreEqual(2, currentPosition.Y);
        }

        [Test]
        public void Player_Moves_Many_Positions_After_Many_Updates()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) },
                    new Position { X = 3, Y = 3, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 20) },
                    new Position { X = 4, Y = 4, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 30) },
                    new Position { X = 5, Y = 5, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 40) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Update();
            updater.Update();
            updater.Update();
            updater.Update();

            var currentPosition = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(5, currentPosition.X);
            Assert.AreEqual(5, currentPosition.Y);
        }

        [Test]
        public void Setting_Time_Stamp_Changes_Time()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Update();
            updater.Time = new DateTime(2, 1, 1, 0, 0, 0, 0);
            updater.Update();

            Assert.AreEqual(new DateTime(2, 1, 1, 0, 0, 0, 0), updater.Time);
            var currentPosition = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(1, currentPosition.X);
            Assert.AreEqual(1, currentPosition.Y);
        }

        [Test]
        public void Game_Moves_Smallest_Increment()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Update();

            Assert.AreEqual(new DateTime(2, 1, 1, 0, 0, 0, 10), updater.Time);
        }

        [Test]
        public void Updates_Multiple_Players()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player1 = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var player2 = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 3, Y = 3, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 4, Y = 4, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Update();

            var position1 = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(2, position1.X);
            Assert.AreEqual(2, position1.Y);

            var position2 = updater.Game.Teams[0].Players[1].CurrentPosition;
            Assert.AreEqual(4, position2.X);
            Assert.AreEqual(4, position2.Y);
        }

        [Test]
        public void Updates_Multiple_Teams()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player1 = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var player2 = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 3, Y = 3, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 4, Y = 4, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team1 = new Team()
            {
                Players = new List<Player>()
                {
                    player1
                }
            };

            var team2 = new Team()
            {
                Players = new List<Player>()
                {
                    player2
                }
            };
            game.Teams.Add(team1);
            game.Teams.Add(team2);

            var updater = new ModelUpdater(game);
            updater.Update();

            var position1 = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(2, position1.X);
            Assert.AreEqual(2, position1.Y);

            var position2 = updater.Game.Teams[1].Players[0].CurrentPosition;
            Assert.AreEqual(4, position2.X);
            Assert.AreEqual(4, position2.Y);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You need to have a team")]
        public void Game_Empty_Teams()
        {
            var game = new Game();
            game.Teams = new List<Team>();

            var updater = new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You need to have a team")]
        public void Game_Null_Teams()
        {
            var game = new Game();

            var updater = new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You must have atleast one player")]
        public void Game_Must_Have_Player_Null()
        {
            var game = new Game();
            game.Teams = new List<Team>();
            game.Teams.Add(new Team());

            var updater = new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You must have atleast one player")]
        public void Game_Must_Have_Player_Empty()
        {
            var game = new Game();
            game.Teams = new List<Team>();
            game.Teams.Add(new Team() 
            { 
                Players = new List<Player>()
            });

            var updater = new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Time out of range")]
        public void Set_Time_Must_Be_Within_Maximum_Position_Range()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 0) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Time = new DateTime(2, 1, 1, 0, 0, 0, 20);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Time out of range")]
        public void Set_Time_Must_Be_Within_Minimum_Position_Range()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 20) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Time = new DateTime(2, 1, 1, 0, 0, 0, 0);
        }

        [Test]
        public void Set_Time_Within_Range()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player
            {
                Positions = new List<Position>
                {
                    new Position { X = 1, Y = 1, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 10) },
                    new Position { X = 2, Y = 2, TimeStamp = new DateTime(2, 1, 1, 0, 0, 0, 20) }
                }
            };
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Time = new DateTime(2, 1, 1, 0, 0, 0, 10);

            Assert.AreEqual(new DateTime(2, 1, 1, 0, 0, 0, 10), updater.Time);
        }
    }
}
