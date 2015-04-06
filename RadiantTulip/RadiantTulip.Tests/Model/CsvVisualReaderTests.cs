using NUnit.Framework;
using RadiantTulip.Model.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class CsvVisualReaderTests
    {
        [Test]
        public void Read_One_Record()
        {
            var file = TestFileHelper.GetFilePath("OnePlayer.csv");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(1, result.Count);
            var team = result[0];
            Assert.AreEqual(1, team.Players.Count);
            Assert.AreEqual("Australia", team.Name);
            var player = team.Players[0];
            Assert.AreEqual("Scott", player.Name);
            Assert.AreEqual(1, player.Positions.Count);
            var position = player.Positions[0];
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);
        }

        [Test]
        public void Read_Two_Records()
        {
            var file = TestFileHelper.GetFilePath("TwoPlayers.csv");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(1, result.Count);
            var team = result[0];
            Assert.AreEqual(2, team.Players.Count);
            Assert.AreEqual("Australia", team.Name);
            var player = team.Players[0];
            Assert.AreEqual("Scott", player.Name);
            Assert.AreEqual(1, player.Positions.Count);
            var position = player.Positions[0];
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);

            player = team.Players[1];
            Assert.AreEqual("Batt", player.Name);
            Assert.AreEqual(1, player.Positions.Count);
            position = player.Positions[0];
            Assert.AreEqual(13.234827, position.X);
            Assert.AreEqual(8.552656, position.Y);
        }

        [Test]
        public void Read_Two_Teams()
        {
            var file = TestFileHelper.GetFilePath("TwoTeams.csv");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(t => t.Name == "New Zealand"));
            Assert.IsTrue(result.Any(t => t.Name == "Australia"));
        }

        [Test]
        public void Read_Multiple_Positions()
        {
            var file = TestFileHelper.GetFilePath("MultiplePositions.csv");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            var player = result[0].Players[0];
            Assert.AreEqual(2, player.Positions.Count);
            var position = player.Positions[0];
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);
            Assert.AreEqual(TimeSpan.Zero, position.TimeStamp);
            position = player.Positions[1];
            Assert.AreEqual(11.314343, position.X);
            Assert.AreEqual(9.703464, position.Y);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 5), position.TimeStamp);
        }
    }
}
