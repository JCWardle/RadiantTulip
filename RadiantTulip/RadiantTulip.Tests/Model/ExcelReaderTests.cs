using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.Model.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class ExcelReaderTests
    {
        [Test]
        public void Read_One_Player()
        {
            List<Team> result;
            using (var stream = GetFilePath("OnePlayer.xlsx"))
            {
                var input = new ExcelReader();
                result = input.GetTeams(stream);
            }

            Assert.AreEqual(1, result.Count);
            var team = result[0];

            Assert.AreEqual(1, team.Players.Count);
            var player = team.Players[0];

            Assert.AreEqual(true, player.Visible);
            Assert.AreEqual(team, player.Team);
            Assert.AreEqual("P1", player.Name);

            Assert.AreEqual(3, player.Positions.Count);
            var positions = player.Positions;
            Assert.AreEqual(-31.94453774, positions[0].Y);
            Assert.AreEqual(115.830865, positions[0].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), positions[0].TimeStamp);

            Assert.AreEqual(-31.94453884, positions[1].Y);
            Assert.AreEqual(115.8308643, positions[1].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 100), positions[1].TimeStamp);

            Assert.AreEqual(-31.94453984, positions[2].Y);
            Assert.AreEqual(115.8308636, positions[2].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 200), positions[2].TimeStamp);
        }

        [Test]
        public void Read_Two_Players()
        {
            List<Team> result;
            using (var stream = GetFilePath("TwoPlayers.xlsx"))
            {
                var input = new ExcelReader();
                result = input.GetTeams(stream);
            }

            Assert.AreEqual(1, result.Count);
            var team = result[0];

            Assert.AreEqual(2, team.Players.Count);
            var player = team.Players[0];

            Assert.AreEqual(true, player.Visible);
            Assert.AreEqual(team, player.Team);
            Assert.AreEqual("P1", player.Name);

            Assert.AreEqual(3, player.Positions.Count);
            var positions = player.Positions;
            Assert.AreEqual(-31.94453774, positions[0].Y);
            Assert.AreEqual(115.830865, positions[0].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), positions[0].TimeStamp);

            Assert.AreEqual(-31.94453884, positions[1].Y);
            Assert.AreEqual(115.8308643, positions[1].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 100), positions[1].TimeStamp);

            Assert.AreEqual(-31.94453984, positions[2].Y);
            Assert.AreEqual(115.8308636, positions[2].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 200), positions[2].TimeStamp);

            player = team.Players[1];

            Assert.AreEqual(true, player.Visible);
            Assert.AreEqual(team, player.Team);
            Assert.AreEqual("P2", player.Name);

            Assert.AreEqual(3, player.Positions.Count);
            positions = player.Positions;
            Assert.AreEqual(-31.94456894, positions[0].Y);
            Assert.AreEqual(115.8304036, positions[0].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), positions[0].TimeStamp);

            Assert.AreEqual(-31.94456894, positions[1].Y);
            Assert.AreEqual(115.8304018, positions[1].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 100), positions[1].TimeStamp);

            Assert.AreEqual(-31.94456884, positions[2].Y);
            Assert.AreEqual(115.8303999, positions[2].X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 200), positions[2].TimeStamp);
        }

        [Test]
        public void Read_Lots_Of_Positions_One_Player()
        {
            List<Team> result;
            using (var stream = GetFilePath("OnePlayerBig.xlsx"))
            {
                var input = new ExcelReader();
                result = input.GetTeams(stream);
            }

            Assert.AreEqual(18510, result[0].Players[0].Positions.Count);
        }

        private Stream GetFilePath(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Concat("RadiantTulip.Tests.TestFiles.", fileName));
        }
    }
}
