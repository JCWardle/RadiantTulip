﻿using NUnit.Framework;
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
                    new Position { X = 100, Y = 100, TimeStamp = new TimeSpan(0, 0, 1) },
                    new Position { X = 200, Y = 200, TimeStamp = new TimeSpan(0, 0, 2) },
                    new Position { X = 300, Y = 300, TimeStamp = new TimeSpan(0, 0, 3) },
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromSeconds(1);
            var parameters = new object[] { player, interval };
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
                    new Position { X = 100, Y = 100, TimeStamp = TimeSpan.FromMilliseconds(100) },
                    new Position { X = 200, Y = 200, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 300, Y = 300, TimeStamp = TimeSpan.FromMilliseconds(300) },
                    new Position { X = 400, Y = 400, TimeStamp = TimeSpan.FromMilliseconds(400) },
                    new Position { X = 500, Y = 500, TimeStamp = TimeSpan.FromMilliseconds(500) },
                    new Position { X = 600, Y = 600, TimeStamp = TimeSpan.FromMilliseconds(600) },
                    new Position { X = 700, Y = 700, TimeStamp = TimeSpan.FromMilliseconds(700) },
                    new Position { X = 800, Y = 800, TimeStamp = TimeSpan.FromMilliseconds(800) },
                    new Position { X = 900, Y = 900, TimeStamp = TimeSpan.FromMilliseconds(900) },
                    new Position { X = 1000, Y = 1000, TimeStamp = TimeSpan.FromMilliseconds(1000) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(1000);
            var parameters = new object[] { player, interval };
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
                    new Position { X = 200, Y = 200, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 300, Y = 300, TimeStamp = TimeSpan.FromMilliseconds(300) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(1000);
            var parameters = new object[] { player, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(0.5d, result);
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
                    new Position { X = 200, Y = 200, TimeStamp = TimeSpan.FromMilliseconds(200) },
                    new Position { X = 300, Y = 300, TimeStamp = TimeSpan.FromMilliseconds(300) }
                }
            };
            player.CurrentPosition = player.Positions.Last();
            var interval = TimeSpan.FromMilliseconds(200);
            var parameters = new object[] { player, interval };
            var speedCalculator = new PlayerSpeed();

            var result = (double)speedCalculator.Convert(parameters, null, null, null);

            Assert.AreEqual(1d, result);
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
