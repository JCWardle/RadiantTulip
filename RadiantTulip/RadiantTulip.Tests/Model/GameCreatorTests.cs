﻿using Moq;
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
                Positions = new List<Position>
                {
                     new Position { X = 1 , Y = 1, TimeStamp = TimeSpan.Zero}
                }
            };
            spatialReader.Setup(s => s.GetTeams(null)).Returns(
                new List<Team>() { new Team { Players = new List<Player> { player } } }
                );
            var coordinateConverter = new Mock<ICoordinateConverter>();
            coordinateConverter.Setup(c => c.Convert(player.Positions[0], ground)).Returns(player.Positions[0]);
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);
            var progressReporter = new MockProgressReporter();
            
            var result = gameCreator.CreateGame(null, ground, progressReporter.ReportProgress);

            coordinateConverter.Verify(c => c.Convert(player.Positions[0], ground), Times.Once);
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
            coordinateConverter.Setup(c => c.Convert(player.Positions[0], ground)).Returns(player.Positions[0]);
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
            coordinateConverter.Setup(c => c.Convert(player.Positions[0], ground)).Returns(player.Positions[0]);
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
            coordinateConverter.Setup(c => c.Convert(player.Positions[0], ground)).Returns(player.Positions[0]);
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var game = gameCreator.CreateGame(null, ground, null);

            Assert.NotNull(game);
        }

        private List<Position> CreatePositions(int positionsRequired)
        {
            var result = new List<Position>();
            var timeStamp = TimeSpan.Zero;
            var x = 0;
            var y = 0;
            for(var i = 0; i < positionsRequired; i++)
            {
                result.Add(new Position
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