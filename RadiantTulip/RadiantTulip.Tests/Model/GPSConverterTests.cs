﻿using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using System;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class GPSConverterTests
    {
        [Test]
        public void Convert_Top_Left_Quadrant()
        {
            var position = new Position
            {
                X = 115.829664d,
                Y = -31.944220d,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.944464,
                CentreLongitude = 115.830156,
                Height = 19100,
                Width = 13200
            };
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.Less(result.X, ground.Width / 2d);
            Assert.Less(result.Y, ground.Height / 2d);
            Assert.AreEqual(1953.6911319460469d, result.X);
            Assert.AreEqual(6834.5015580400923d, result.Y);
            Assert.AreEqual(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
        }

        [Test]
        public void Convert_Top_Right_Quadrant()
        {
            var position = new Position
            {
                Y = -31.944255,
                X = 115.830572,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.944464,
                CentreLongitude = 115.830156,
                Height = 19100,
                Width = 13200
            };
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.Greater(result.X, ground.Width / 2d);
            Assert.Less(result.Y, ground.Height / 2d);
            Assert.AreEqual(10528.586359803765d, result.X);
            Assert.AreEqual(7224.0197770889663d, result.Y);
            Assert.AreEqual(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
        }

        [Test]
        public void Convert_Bottom_Right_Quadrant()
        {
            var position = new Position
            {
                Y = -31.944667,
                X = 115.830706,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.944464,
                CentreLongitude = 115.830156,
                Height = 19100,
                Width = 13200
            };
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.Greater(result.X, ground.Width / 2d);
            Assert.Greater(result.Y, ground.Height / 2d);
            Assert.AreEqual(11794.044466309591d, result.X);
            Assert.AreEqual(11809.205670936542d, result.Y);
            Assert.AreEqual(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
        }

        [Test]
        public void Convert_Bottom_Left_Quadrant()
        {
            var position = new Position
            {
                Y = -31.944745,
                X = 115.829690,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.944464,
                CentreLongitude = 115.830156,
                Height = 19100,
                Width = 13200
            };
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.Less(result.X, ground.Width / 2d);
            Assert.Greater(result.Y, ground.Height / 2d);
            Assert.AreEqual(2199.2277794483516d, result.X);
            Assert.AreEqual(12677.274845047472d, result.Y);
            Assert.AreEqual(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
        }

        [Test]
        public void Adjusts_For_Ground_Rotation_Y()
        {
            var position = new Position
            {
                Y = -31.0000009,
                X = 115.0,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.0,
                CentreLongitude = 115.0,
                Height = 500,
                Width = 1000,
                Rotation = 180
            };

            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(500, result.X);
            Assert.AreEqual(239.9838172250644d, result.Y);
        }

        [Test]
        public void Adjusts_For_Ground_Rotation_X()
        {
            var position = new Position
            {
                Y = -31,
                X = 115.00001049,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.0,
                CentreLongitude = 115.0,
                Height = 500,
                Width = 1000,
                Rotation = 180
            };

            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(399.93071071314364d, result.X);
            Assert.AreEqual(250.00000000000009d, result.Y);
        }

        [Test]
        public void Adjusts_For_Ground_Rotation_X_Y()
        {
            var position = new Position
            {
                Y = -31.0000009,
                X = 115.00001049,
                TimeStamp = new TimeSpan(0, 0, 0, 0, 0)
            };
            var ground = new Ground()
            {
                CentreLatitude = -31.0,
                CentreLongitude = 115.0,
                Height = 500,
                Width = 1000,
                Rotation = 180
            };

            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(399.93071071314364d, result.X);
            Assert.AreEqual(239.98381722506443d, result.Y);
        }
    }
}
