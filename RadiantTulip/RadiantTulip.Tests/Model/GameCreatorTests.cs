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
        public void Creates_Game()
        {
            var spatialReader = new Mock<ISpatialReader>();
            spatialReader.Setup(s => s.GetTeams(null)).Returns(new List<Team>());
            var coordinateConverter = new Mock<ICoordinateConverter>();
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var result = gameCreator.CreateGame(null);

            Assert.IsNotNull(result);
        }

        [Test]
        public void Creates_Game_Detailed_Game()
        {
            var position = new Position
            {
                X = 1,
                Y = 2
            };

            var spatialReader = new Mock<ISpatialReader>();
            spatialReader.Setup(s => s.GetTeams(null)).Returns(
            new List<Team>
            {
                new Team 
                { 
                    Name = "test", 
                    Players = new List<Player> 
                    { 
                        new Player
                        { 
                            Visible = true,
                            Positions = new List<Position>
                            {
                                position                             
                            }
                        } 
                    }
                }
            });

            var coordinateConverter = new Mock<ICoordinateConverter>();
            coordinateConverter.Setup(c => c.Convert(position)).Returns(new Position
            {
                X = 3,
                Y = 4
            });
            
            var gameCreator = new GameCreator(coordinateConverter.Object, spatialReader.Object);

            var result = gameCreator.CreateGame(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Teams.Count);

            var team = result.Teams.First();
            Assert.AreEqual("test", team.Name);
            Assert.AreEqual(1, team.Players.Count);

            var player = team.Players.First();
            Assert.AreEqual(true, player.Visible);
            Assert.AreEqual(1, player.Positions.Count);

            position = player.Positions.First();
            Assert.AreEqual(position.X, 3);
            Assert.AreEqual(position.Y, 4);
        }
    }
}