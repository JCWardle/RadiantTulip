using Moq;
using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
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
            
            var result = gameCreator.CreateGame(null, ground);

            coordinateConverter.Verify(c => c.Convert(player.Positions[0], ground), Times.Once);
            spatialReader.Verify(s => s.GetTeams(null), Times.Once);
            Assert.AreEqual(ground, result.Ground);
        }
    }
}