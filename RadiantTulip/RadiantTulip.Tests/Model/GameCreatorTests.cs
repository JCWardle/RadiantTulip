using Moq;
using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using RadiantTulip.Tests.Model.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class GameCreatorTests
    {
        [Test]
        public void Creates_Game_With_Correct_Teams()
        {
            var spatialReader = new Mock<ISpatialReader>();
            var ground = new Ground();
            var player = new Player() 
            {
                Positions = new LinkedList<Position>()
            };
            player.Positions.AddLast(new Position { X = 1, Y = 1, TimeStamp = TimeSpan.Zero });
            spatialReader.Setup(s => s.GetTeams(null)).Returns(
                new List<Team>() { new Team { Players = new List<Player> { player } } }
                );
            var coordinateConverter = new Mock<ICoordinateConverter>();
            coordinateConverter.Setup(c => c.Convert(player.Positions.First.Value, ground)).Returns(player.Positions.First.Value);
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);
            var progressReporter = new MockProgressReporter();
            
            var result = gameCreator.CreateGame(null, ground, progressReporter.ReportProgress);

            coordinateConverter.Verify(c => c.Convert(player.Positions.First.Value, ground), Times.Once);
            spatialReader.Verify(s => s.GetTeams(null), Times.Once);
            Assert.AreEqual(ground, result.Ground);
        }

        [Test]
        public void Create_Game_Increments_Progress()
        {
            var spatialReader = new Mock<ISpatialReader>();
            var ground = new Ground();
            var player = new Player()
            {
                Positions = CreatePositions(20)
            };
            spatialReader.Setup(s => s.GetTeams(null)).Returns(
                new List<Team>() { new Team { Players = new List<Player> { player } } }
            );
            var coordinateConverter = new Mock<ICoordinateConverter>();
            coordinateConverter.Setup(c => c.Convert(It.IsAny<Position>(), ground)).Returns(player.Positions.First.Value);
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);
            var progressReporter = new MockProgressReporter();

            gameCreator.CreateGame(null, ground, progressReporter.ReportProgress);

            Assert.AreEqual(100, progressReporter.Progress);
            Assert.AreEqual(3, progressReporter.Calls);
        }

        [Test]
        public void Create_Game_Increments_Progress_For_Heaps_Of_Positions()
        {
            var spatialReader = new Mock<ISpatialReader>();
            var ground = new Ground();
            var player = new Player()
            {
                Positions = CreatePositions(300)
            };
            spatialReader.Setup(s => s.GetTeams(null)).Returns(
                new List<Team>() { new Team { Players = new List<Player> { player } } }
            );
            var coordinateConverter = new Mock<ICoordinateConverter>();
            coordinateConverter.Setup(c => c.Convert(It.IsAny<Position>(), ground)).Returns(player.Positions.First.Value);
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);
            var progressReporter = new MockProgressReporter();

            gameCreator.CreateGame(null, ground, progressReporter.ReportProgress);

            Assert.AreEqual(100, progressReporter.Progress);
            Assert.AreEqual(51, progressReporter.Calls);
        }

        [Test]
        public void Create_Game_Null_Progress_Reporter()
        {
            var spatialReader = new Mock<ISpatialReader>();
            var ground = new Ground();
            var player = new Player()
            {
                Positions = CreatePositions(20)
            };
            spatialReader.Setup(s => s.GetTeams(null)).Returns(
                new List<Team>() { new Team { Players = new List<Player> { player } } }
            );
            var coordinateConverter = new Mock<ICoordinateConverter>();
            coordinateConverter.Setup(c => c.Convert(It.IsAny<Position>(), ground)).Returns(player.Positions.First.Value);
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var game = gameCreator.CreateGame(null, ground, null);

            Assert.NotNull(game);
        }

        [Test]
        public void Sets_Ball_From_Reader()
        {
            var ball = new Ball();
            var spatialReader = new Mock<ISpatialReader>();
            var coordinateConverter = new Mock<ICoordinateConverter>();
            var ground = new Ground();
            spatialReader.Setup(s => s.GetTeams(null)).Returns(new List<Team>());
            spatialReader.Setup(s => s.GetBall(null)).Returns(ball);

            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var game = gameCreator.CreateGame(null, ground, null);

            Assert.AreEqual(ball, game.Ball);
        }

        [Test]
        public void Converts_Ball_Positions()
        {
            var ball = new Ball { Positions = CreatePositions(3) };
            var spatialReader = new Mock<ISpatialReader>();
            var coordinateConverter = new Mock<ICoordinateConverter>();
            var ground = new Ground();
            spatialReader.Setup(s => s.GetTeams(null)).Returns(new List<Team>());
            spatialReader.Setup(s => s.GetBall(null)).Returns(ball);
            coordinateConverter.Setup(c => c.Convert(It.IsAny<Position>(), ground)).Returns(new Position
            {
                X = 10,
                Y = 10,
                TimeStamp = TimeSpan.FromMilliseconds(10)
            });

            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var game = gameCreator.CreateGame(null, ground, null);

            Assert.IsNotNull(game.Ball);
            ball = game.Ball;
            Assert.AreEqual(10, ball.Positions.First.Value.X);
            Assert.AreEqual(10, ball.Positions.First.Value.Y);
            Assert.AreEqual(TimeSpan.FromMilliseconds(10), ball.Positions.First.Value.TimeStamp);
        }

        [Test]
        public void Handles_Ball_With_No_Positions()
        {
            var ball = new Ball { Positions = null };
            var spatialReader = new Mock<ISpatialReader>();
            var coordinateConverter = new Mock<ICoordinateConverter>();
            var ground = new Ground();
            spatialReader.Setup(s => s.GetTeams(null)).Returns(new List<Team>());
            spatialReader.Setup(s => s.GetBall(null)).Returns(ball);

            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var game = gameCreator.CreateGame(null, ground, null);

            Assert.IsNotNull(game.Ball);
        }

        private LinkedList<Position> CreatePositions(int positionsRequired)
        {
            var result = new LinkedList<Position>();
            var timeStamp = TimeSpan.Zero;
            var x = 0;
            var y = 0;
            for(var i = 0; i < positionsRequired; i++)
            {
                result.AddLast(new Position
                {
                    TimeStamp = timeStamp,
                    X = x,
                    Y = y
                });

                x++;
                y++;
                timeStamp += new TimeSpan(0, 0, 0, 0, 10);
            }

            return result;
        }
    }
}