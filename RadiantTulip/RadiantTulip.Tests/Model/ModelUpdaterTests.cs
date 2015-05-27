using NUnit.Framework;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class ModelUpdaterTests
    {
        [Test]
        public void Game_Getter()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), updater.Time);
        }

        [Test]
        public void Player_Starts_At_First_Position()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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
            Assert.AreEqual(1, currentPosition.Value.X);
            Assert.AreEqual(1, currentPosition.Value.Y);
        }

        [Test]
        public void Player_Moves_Next_Position_After_Update()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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
            Assert.AreEqual(2, currentPosition.Value.X);
            Assert.AreEqual(2, currentPosition.Value.Y);
        }

        [Test]
        public void Player_Moves_Many_Positions_After_Many_Updates()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.Positions.AddLast(new Position { X = 3, Y = 3, TimeStamp = new TimeSpan(0, 0, 0, 0, 20) });
            player.Positions.AddLast(new Position { X = 4, Y = 4, TimeStamp = new TimeSpan(0, 0, 0, 0, 30) });
            player.Positions.AddLast(new Position { X = 5, Y = 5, TimeStamp = new TimeSpan(0, 0, 0, 0, 40) });
            player.PositionsLookup = FillLookup(player.Positions);
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
            Assert.AreEqual(5, currentPosition.Value.X);
            Assert.AreEqual(5, currentPosition.Value.Y);
        }

        [Test]
        public void Setting_Time_Stamp_Changes_Time()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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
            updater.Time = new TimeSpan(0, 0, 0, 0, 0);
            updater.Update();

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), updater.Time);
            var currentPosition = updater.Game.Teams[0].Players[0].CurrentPosition;
            Assert.AreEqual(1, currentPosition.Value.X);
            Assert.AreEqual(1, currentPosition.Value.Y);
        }

        [Test]
        public void Game_Moves_Smallest_Increment()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 10), updater.Time);
        }

        [Test]
        public void Updates_Multiple_Players()
        {
            var game = CreateGame();
            var player1 = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player1.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player1.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player1.PositionsLookup = FillLookup(player1.Positions);
            var player2 = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player2.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player2.Positions.AddLast(new Position { X = 4, Y = 4, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player2.PositionsLookup = FillLookup(player2.Positions);
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
            Assert.AreEqual(2, position1.Value.X);
            Assert.AreEqual(2, position1.Value.Y);

            var position2 = updater.Game.Teams[0].Players[1].CurrentPosition;
            Assert.AreEqual(4, position2.Value.X);
            Assert.AreEqual(4, position2.Value.Y);
        }

        [Test]
        public void Updates_Multiple_Teams()
        {
            var game = CreateGame();
            var player1 = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player1.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player1.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player1.PositionsLookup = FillLookup(player1.Positions);
            var player2 = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player2.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player2.Positions.AddLast(new Position { X = 4, Y = 4, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player2.PositionsLookup = FillLookup(player2.Positions);
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
            Assert.AreEqual(2, position1.Value.X);
            Assert.AreEqual(2, position1.Value.Y);

            var position2 = updater.Game.Teams[1].Players[0].CurrentPosition;
            Assert.AreEqual(4, position2.Value.X);
            Assert.AreEqual(4, position2.Value.Y);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You need to have a team")]
        public void Game_Empty_Teams()
        {
            var game = new Game();
            game.Teams = new List<Team>();

            new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You need to have a team")]
        public void Game_Null_Teams()
        {
            var game = new Game();

            new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "You must have atleast one player")]
        public void Game_Must_Have_Player_Null()
        {
            var game = new Game();
            game.Teams = new List<Team>();
            game.Teams.Add(new Team());

            new ModelUpdater(game);
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

            new ModelUpdater(game);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Time out of range")]
        public void Set_Time_Must_Be_Within_Maximum_Position_Range()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Time = new TimeSpan(0, 0, 0, 0, 20);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Time out of range")]
        public void Set_Time_Must_Be_Within_Minimum_Position_Range()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Time = new TimeSpan(0, 0, 0, 0, 20);
        }

        [Test]
        public void Set_Time_Within_Range()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);
            updater.Time = new TimeSpan(0, 0, 0, 0, 10);

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 10), updater.Time);
        }

        [Test]
        public void Update_At_End()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 10), updater.Time);
        }

        [Test]
        public void Max_Returns_Highest_Position_Time()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 10), updater.MaxTime);
        }

        [Test]
        public void Increment_Returns_Small_Increment()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            Assert.AreEqual(10, updater.Increment.Milliseconds);
        }

        [Test]
        public void Increment_Returns_Big_Increment()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 30, 0) });
            player.PositionsLookup = FillLookup(player.Positions);
            var team = new Team()
            {
                Players = new List<Player>()
                {
                    player
                }
            };
            game.Teams.Add(team);

            var updater = new ModelUpdater(game);

            Assert.AreEqual(30, updater.Increment.Seconds);
        }

        [Test]
        public void Plays_Backwards_At_Max_Time()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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

            updater.ChangeDirection();
            updater.Update();

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), updater.Time);
        }

        [Test]
        public void Plays_Backwards()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 20) });
            player.PositionsLookup = FillLookup(player.Positions);
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

            updater.ChangeDirection();
            updater.Update();

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), updater.Time);
        }

        [Test]
        public void Goes_Back_Goes_Forward()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 20) });
            player.PositionsLookup = FillLookup(player.Positions);
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

            updater.ChangeDirection();
            updater.Update();
            updater.ChangeDirection();
            updater.Update();
            updater.Update();

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 20), updater.Time);
        }

        [Test]
        public void Rewinds_To_Start_And_Stops()
        {
            var game = CreateGame();
            var player = new Player
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
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

            updater.ChangeDirection();
            updater.Update();
            updater.Update();

            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), updater.Time);
        }

        [Test]
        public void Updates_Ball_Current_Position_To_A_Value()
        {
            var game = new Game() { Teams = new List<Team>(), Ball = new Ball() };
            game.Ball = new Ball() { Positions = new LinkedList<Position>() };
            game.Ball.Positions.AddLast(new Position { TimeStamp = TimeSpan.FromMilliseconds(10), Y = 10, X = 10 });
            game.Ball.PositionsLookup = FillLookup(game.Ball.Positions);
            var player = new Player { Positions = new LinkedList<Position> () };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            game.Teams.Add(new Team { Players = new List<Player> { player } });

            var updater = new ModelUpdater(game);
            updater.Update();
            updater.Update();

            Assert.AreEqual(10, game.Ball.CurrentPosition.Value.X);
            Assert.AreEqual(10, game.Ball.CurrentPosition.Value.Y);
            Assert.AreEqual(TimeSpan.FromMilliseconds(10), game.Ball.CurrentPosition.Value.TimeStamp);
        }

        [Test]
        public void Updates_Ball_Current_Position_To_Null_With_No_Value()
        {
            var game = new Game() { Teams = new List<Team>() };
            game.Ball = new Ball() { Positions = new LinkedList<Position>() };
            game.Ball.Positions.AddLast(new Position { TimeStamp = TimeSpan.FromMilliseconds(10), Y = 10, X = 10 });
            game.Ball.PositionsLookup = FillLookup(game.Ball.Positions);

            var player = new Player { Positions = new LinkedList<Position>() };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 20) });
            player.PositionsLookup = FillLookup(player.Positions);
            game.Teams.Add(new Team { Players = new List<Player> { player } });

            var updater = new ModelUpdater(game);
            updater.Update();
            updater.Update();
            updater.Update();

            Assert.IsNull(game.Ball.CurrentPosition);
        }

        [Test]
        public void Updater_Handles_Null_Ball()
        {
            var game = new Game() { Teams = new List<Team>() };
            var player = new Player { Positions = new LinkedList<Position>() };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            game.Teams.Add(new Team { Players = new List<Player> { player } });

            var updater = new ModelUpdater(game);
            updater.Update();

            Assert.AreEqual(2, player.CurrentPosition.Value.X);
            Assert.AreEqual(2, player.CurrentPosition.Value.Y);
            Assert.AreEqual(TimeSpan.FromMilliseconds(10), player.CurrentPosition.Value.TimeStamp);
        }

        [Test]
        public void Updater_Handles_No_Ball_Positions()
        {
            var game = new Game() { Teams = new List<Team>(), Ball = new Ball() };
            var player = new Player { Positions = new LinkedList<Position>() };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = new TimeSpan(0, 0, 0, 0, 0) });
            player.Positions.AddLast(new Position { X = 2, Y = 2, TimeStamp = new TimeSpan(0, 0, 0, 0, 10) });
            player.PositionsLookup = FillLookup(player.Positions);
            game.Teams.Add(new Team { Players = new List<Player> { player } });

            var updater = new ModelUpdater(game);
            updater.Update();

            Assert.AreEqual(2, player.CurrentPosition.Value.X);
            Assert.AreEqual(2, player.CurrentPosition.Value.Y);
            Assert.AreEqual(TimeSpan.FromMilliseconds(10), player.CurrentPosition.Value.TimeStamp);
        }

        private Game CreateGame()
        {
            var result = new Game()
            {
                Teams = new List<Team>(),
                Ball = new Ball
                {
                    Positions = new LinkedList<Position>(),
                    PositionsLookup = new Dictionary<TimeSpan,LinkedListNode<Position>>()
                }
            }; 
            result.Ball.Positions.AddLast(new Position { TimeStamp = TimeSpan.FromMilliseconds(10), X = 10, Y = 10});
            return result;
        }

        private Dictionary<TimeSpan, LinkedListNode<Position>> FillLookup(LinkedList<Position> positions)
        {
            var result = new Dictionary<TimeSpan, LinkedListNode<Position>>();
            LinkedListNode<Position> position = null;

            do
            {
                if (position == null)
                    position = positions.First;
                else
                    position = position.Next;

                result.Add(position.Value.TimeStamp, position);

            } while (position != positions.Last);

            return result;
        }
    }
}
