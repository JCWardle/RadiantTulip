using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class PlayerSpeedTests
    {
        [Test]
        public void Calculates_Player_Speed_In_Metres_Per_Second_With_Seconds()
        {
            var player = new Player
            {
                Positions = new List<Position> 
                {
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.Zero},
                    new Position { X = 100, Y = 0, TimeStamp = new TimeSpan(0, 0, 1) },
                    new Position { X = 200, Y = 0, TimeStamp = new TimeSpan(0, 0, 2) },
                    new Position { X = 300, Y = 0, TimeStamp = new TimeSpan(0, 0, 3) },
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromSeconds(1);
            var parameters = new object[] { new ObservableCollection<Player> { player }, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(1d, result);
        }

        [Test]
        public void Calculates_Player_Speed_In_Metres_Per_Second_With_Milliseconds()
        {
            var player = new Player
            {
                Positions = new List<Position> 
                {
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.Zero},
                    new Position { X = 10, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(100) },
                    new Position { X = 20, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 30, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(300) },
                    new Position { X = 40, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(400) },
                    new Position { X = 50, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(500) },
                    new Position { X = 60, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(600) },
                    new Position { X = 70, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(700) },
                    new Position { X = 80, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(800) },
                    new Position { X = 90, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(900) },
                    new Position { X = 100, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(1000) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(1000);
            var parameters = new object[] { new ObservableCollection<Player> { player }, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(1d, result);
        }

        [Test]
        public void Calculates_Player_Speed_Average_Over_Time()
        {
            var player = new Player
            {
                Positions = new List<Position> 
                {
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.Zero},
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(100) },
                    new Position { X = 0, Y = 10, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 0, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(300) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(1000);
            var parameters = new object[] { new ObservableCollection<Player> { player }, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(0.2d, result);
        }

        [Test]
        public void Calculates_Player_Speed_Ignores_Positions_Outside_Interval()
        {
            var player = new Player
            {
                Positions = new List<Position> 
                {
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.Zero},
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.FromMilliseconds(100) },
                    new Position { X = 0, Y = 20, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 0, Y = 30, TimeStamp = TimeSpan.FromMilliseconds(300) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(100);
            var parameters = new object[] { new ObservableCollection<Player> { player }, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(1d, result);
        }

        [Test]
        public void Doesnt_Calculate_Player_Speed_With_Null_Player()
        {
            var interval = TimeSpan.FromMilliseconds(100);
            var parameters = new object[] { new ObservableCollection<Player>(), interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(0d, result);
        }

        [Test]
        public void Rounds_Player_Speed_To_Two_Decimal_Places()
        {
            var player = new Player
            {
                Positions = new List<Position> 
                {
                    new Position { X = 0, Y = 0, TimeStamp = TimeSpan.Zero},
                    new Position { X = 0, Y = 3, TimeStamp = TimeSpan.FromMilliseconds(100) },
                    new Position { X = 0, Y = 15, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 0, Y = 43, TimeStamp = TimeSpan.FromMilliseconds(300) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(400);
            var parameters = new object[] { new ObservableCollection<Player> { player }, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(1.08d, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Cant_Convert_Back_Player_Speed()
        {
            var speed = new PlayerSpeed();
            speed.ConvertBack(null, null, null, null);
        }
    }
}
