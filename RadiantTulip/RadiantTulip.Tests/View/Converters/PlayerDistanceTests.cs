using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class PlayerDistanceTests
    {
        [Test]
        public void Player_Has_No_Positions()
        {
            var player = new Player { Positions = new List<Position>() };
            var converter = new PlayerDistance();
            var parameters = new object[] { player, TimeSpan.Zero };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Player_With_Two_Positions()
        {
            var player = new Player()
            {
                Positions = new List<Position>()
                {
                    new Position { X = 10, Y = 10, TimeStamp = TimeSpan.Zero },
                    new Position { X = 20, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(10) }
                }
            };
            var converter = new PlayerDistance();
            var parameters = new object[] { player, TimeSpan.FromMilliseconds(10) };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(14.14, result);
        }

        [Test]
        public void Ignores_Positions_After_Current_Time()
        {
            var player = new Player()
            {
                Positions = new List<Position>()
                {
                    new Position { X = 10, Y = 10, TimeStamp = TimeSpan.Zero },
                    new Position { X = 20, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(10) },
                    new Position { X = 20, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(20) }
                }
            };
            var converter = new PlayerDistance();
            var parameters = new object[] { player, TimeSpan.FromMilliseconds(10) };

            var result = (double)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(14.14, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Player_Distance_Convert_Back_Not_Implemented()
        {
            var converter = new PlayerDistance();
            converter.ConvertBack(null, null, null, null);
        }
    }
}
