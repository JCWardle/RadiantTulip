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
            using (var stream = TestFileHelper.GetFilePath("OnePlayer.xlsx"))
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
            var position = positions.First;
            Assert.AreEqual(-31.94453774, position.Value.Y);
            Assert.AreEqual(115.830865, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), position.Value.TimeStamp);

            position = position.Next;
            Assert.AreEqual(-31.94453884, position.Value.Y);
            Assert.AreEqual(115.8308643, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 100), position.Value.TimeStamp);

            position = position.Next;
            Assert.AreEqual(-31.94453984, position.Value.Y);
            Assert.AreEqual(115.8308636, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 200), position.Value.TimeStamp);
        }

        [Test]
        public void Player_Positions_Get_Added_To_The_Lookup()
        {
            var file = TestFileHelper.GetFilePath("OnePlayer.xlsx");
            var reader = new ExcelReader();

            var result = reader.GetTeams(file);

            var player = result[0].Players[0];
            Assert.AreEqual(3, player.PositionsLookup.Keys.Count);
            Assert.AreEqual(-31.94453774, player.PositionsLookup[TimeSpan.Zero].Value.Y);
            Assert.AreEqual(115.830865, player.PositionsLookup[TimeSpan.Zero].Value.X);
            Assert.AreEqual(-31.94453884, player.PositionsLookup[new TimeSpan(0, 0, 0, 0, 100)].Value.Y);
            Assert.AreEqual(115.8308643, player.PositionsLookup[new TimeSpan(0, 0, 0, 0, 100)].Value.X);
            Assert.AreEqual(-31.94453984, player.PositionsLookup[new TimeSpan(0, 0, 0, 0, 200)].Value.Y);
            Assert.AreEqual(115.8308636, player.PositionsLookup[new TimeSpan(0, 0, 0, 0, 200)].Value.X);
        }

        [Test]
        public void Read_Two_Players()
        {
            List<Team> result;
            using (var stream = TestFileHelper.GetFilePath("TwoPlayers.xlsx"))
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
            var position = positions.First;
            Assert.AreEqual(-31.94453774, position.Value.Y);
            Assert.AreEqual(115.830865, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), position.Value.TimeStamp);

            position = position.Next;
            Assert.AreEqual(-31.94453884, position.Value.Y);
            Assert.AreEqual(115.8308643, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 100), position.Value.TimeStamp);

            position = position.Next;
            Assert.AreEqual(-31.94453984, position.Value.Y);
            Assert.AreEqual(115.8308636, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 200), position.Value.TimeStamp);

            player = team.Players[1];

            Assert.AreEqual(true, player.Visible);
            Assert.AreEqual(team, player.Team);
            Assert.AreEqual("P2", player.Name);

            Assert.AreEqual(3, player.Positions.Count);
            position = player.Positions.First;
            Assert.AreEqual(-31.94456894, position.Value.Y);
            Assert.AreEqual(115.8304036, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 0), position.Value.TimeStamp);

            position = position.Next;
            Assert.AreEqual(-31.94456894, position.Value.Y);
            Assert.AreEqual(115.8304018, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 100), position.Value.TimeStamp);

            position = position.Next;
            Assert.AreEqual(-31.94456884, position.Value.Y);
            Assert.AreEqual(115.8303999, position.Value.X);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 200), position.Value.TimeStamp);
        }

        [Test]
        public void Read_Lots_Of_Positions_One_Player()
        {
            List<Team> result;
            using (var stream = TestFileHelper.GetFilePath("OnePlayerBig.xlsx"))
            {
                var input = new ExcelReader();
                result = input.GetTeams(stream);
            }

            Assert.AreEqual(18510, result[0].Players[0].Positions.Count);
        }

        [Test]
        public void Ball_Is_Null_Because_Not_Implemented()
        {
            Ball result;
            using (var stream = TestFileHelper.GetFilePath("OnePlayerBig.xlsx"))
            {
                var input = new ExcelReader();
                result = input.GetBall(stream);
            }

            Assert.IsNull(result);
        }
    }
}
