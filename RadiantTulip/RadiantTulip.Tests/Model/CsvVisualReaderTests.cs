using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.Model.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class CsvVisualReaderTests
    {
        [Test]
        public void Read_One_Record()
        {
            var file = TestFileHelper.GetFilePath("OnePlayer.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(1, result.Count);
            var team = result[0];
            Assert.AreEqual(1, team.Players.Count);
            Assert.AreEqual("Australia", team.Name);
            var player = team.Players[0];
            Assert.AreEqual("Scott", player.Name);
            Assert.AreEqual(1, player.Positions.Count);
            Assert.IsTrue(player.Visible);
            Assert.AreEqual(Size.Medium, player.Size);
            Assert.AreEqual(PlayerShape.Circle, player.Shape);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), player.Colour);
            var position = player.Positions.First.Value;
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);
        }

        [Test]
        public void Read_Two_Records()
        {
            var file = TestFileHelper.GetFilePath("TwoPlayers.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(1, result.Count);
            var team = result[0];
            Assert.AreEqual(2, team.Players.Count);
            Assert.AreEqual("Australia", team.Name);
            var player = team.Players[0];
            Assert.AreEqual("Scott", player.Name);
            Assert.AreEqual(1, player.Positions.Count);
            Assert.IsTrue(player.Visible);
            Assert.AreEqual(Size.Medium, player.Size);
            Assert.AreEqual(PlayerShape.Circle, player.Shape);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), player.Colour);
            var position = player.Positions.First.Value;
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);

            player = team.Players[1];
            Assert.AreEqual("Batt", player.Name);
            Assert.AreEqual(1, player.Positions.Count);
            Assert.IsTrue(player.Visible);
            Assert.AreEqual(Size.Medium, player.Size);
            Assert.AreEqual(PlayerShape.Circle, player.Shape);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), player.Colour);
            position = player.Positions.First.Value;
            Assert.AreEqual(13.234827, position.X);
            Assert.AreEqual(8.552656, position.Y);
        }

        [Test]
        public void Player_Positions_Get_Added_To_The_Lookup()
        {
            var file = TestFileHelper.GetFilePath("MultiplePositions.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            var player = result[0].Players[0];
            Assert.AreEqual(2, player.PositionsLookup.Keys.Count);
            Assert.AreEqual(11.299328, player.PositionsLookup[TimeSpan.Zero].Value.X);
            Assert.AreEqual(9.845711, player.PositionsLookup[TimeSpan.Zero].Value.Y);
            Assert.AreEqual(11.314343, player.PositionsLookup[new TimeSpan(0, 0, 0, 0, 40)].Value.X);
            Assert.AreEqual(9.703464, player.PositionsLookup[new TimeSpan(0, 0, 0, 0, 40)].Value.Y);
        }

        [Test]
        public void Read_Two_Teams()
        {
            var file = TestFileHelper.GetFilePath("TwoTeams.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(t => t.Name == "New Zealand"));
            Assert.IsTrue(result.Any(t => t.Name == "Australia"));
        }

        [Test]
        public void Positions_Start_At_Zero()
        {
            var file = TestFileHelper.GetFilePath("NotStartingAtZero.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            Assert.AreEqual(1, result.Count);
            var team = result.First();
            Assert.AreEqual(1, team.Players.Count);
            var player = team.Players.First();
            Assert.AreEqual(1, player.Positions.Count);
            var position = player.Positions.First.Value;
            Assert.AreEqual(TimeSpan.Zero, position.TimeStamp);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "A number in line 1 is not in a numerical format")]
        public void Read_Invalid_Frame()
        {
            var file = TestFileHelper.GetFilePath("InvalidFrame.txt");
            var reader = new CsvVisualReader();

            reader.GetTeams(file);
        }

        [Test]
        public void Read_Multiple_Positions()
        {
            var file = TestFileHelper.GetFilePath("MultiplePositions.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            var player = result[0].Players[0];
            Assert.AreEqual(2, player.Positions.Count);
            var position = player.Positions.First.Value;
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);
            Assert.AreEqual(TimeSpan.Zero, position.TimeStamp);
            position = player.Positions.First.Next.Value;
            Assert.AreEqual(11.314343, position.X);
            Assert.AreEqual(9.703464, position.Y);
            Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 40), position.TimeStamp);
        }

        [Test]
        public void Teams_Have_Different_Default_Colours()
        {
            var file = TestFileHelper.GetFilePath("TwoTeams.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetTeams(file);

            var player = result[0].Players[0];
            Assert.AreEqual(Color.FromRgb(0, 0, 0), player.Colour);
            player = result[1].Players[0];
            Assert.AreEqual(Color.FromRgb(255, 255, 255), player.Colour);
        }

        [Test]
        public void Ball_Is_Added_At_First_Line()
        {
            var file = TestFileHelper.GetFilePath("TwoBallPositions.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetBall(file);

            Assert.AreEqual(Color.FromArgb(255, 255, 255, 0), result.Colour);
            Assert.AreEqual(2, result.Positions.Count());
            var position = result.Positions.First.Value;
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);
            Assert.AreEqual(TimeSpan.Zero, position.TimeStamp);
        }

        [Test]
        public void Multiple_Ball_Locations()
        {
            var file = TestFileHelper.GetFilePath("TwoBallPositions.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetBall(file);

            Assert.AreEqual(Color.FromArgb(255, 255, 255, 0), result.Colour);
            Assert.AreEqual(2, result.Positions.Count());
            var position = result.Positions.First.Value;
            Assert.AreEqual(11.299328, position.X);
            Assert.AreEqual(9.845711, position.Y);
            Assert.AreEqual(TimeSpan.Zero, position.TimeStamp);
            position = result.Positions.Last.Value;
            Assert.AreEqual(13.234827, position.X);
            Assert.AreEqual(8.552656, position.Y);
            Assert.AreEqual(TimeSpan.FromMilliseconds(40), position.TimeStamp);
        }

        [Test]
        public void Ball_Mixed_In_With_Players_Missing_Frames()
        {
            var file = TestFileHelper.GetFilePath("BallMixedInWithPlayersMissingFrames.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetBall(file);

            Assert.AreEqual(Color.FromArgb(255, 255, 255, 0), result.Colour);
            Assert.AreEqual(8, result.Positions.Count());
        }

        [Test]
        public void Adds_Ball_Positions_To_A_Lookup()
        {
            var file = TestFileHelper.GetFilePath("TwoBallPositions.txt");
            var reader = new CsvVisualReader();

            var result = reader.GetBall(file);

            Assert.AreEqual(2, result.PositionsLookup.Keys.Count);
            Assert.AreEqual(11.299328, result.PositionsLookup[TimeSpan.Zero].Value.X);
            Assert.AreEqual(9.845711, result.PositionsLookup[TimeSpan.Zero].Value.Y);
            Assert.AreEqual(13.234827, result.PositionsLookup[TimeSpan.FromMilliseconds(40)].Value.X);
            Assert.AreEqual(8.552656, result.PositionsLookup[TimeSpan.FromMilliseconds(40)].Value.Y);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "A number in line 2 is not in a numerical format")]
        public void Read_OverFlowed_Positional_Information()
        {
            var file = TestFileHelper.GetFilePath("OverflowPosition.txt");
            var reader = new CsvVisualReader();

            reader.GetTeams(file);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "A number in line 2 is not in a numerical format")]
        public void Read_Missing_Positional_Information()
        {
            var file = TestFileHelper.GetFilePath("NoPositionalData.txt");
            var reader = new CsvVisualReader();

            reader.GetTeams(file);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Positional data on line 2 doesn't have enough columns, it requires 6 or more")]
        public void Read_Missing_Columns()
        {
            var file = TestFileHelper.GetFilePath("NotEnoughColumns.txt");
            var reader = new CsvVisualReader();

            reader.GetTeams(file);
        }
    }
}
