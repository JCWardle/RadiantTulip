using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class VisualDataConverterTests
    {
        [Test]
        public void Convert_Position()
        {
            var position = new Position { TimeStamp = TimeSpan.Zero, X = 1, Y = 1 };
            var ground = new Ground() { Width = 2800, Height = 1500 };
            var converter = new VisualDataConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(1400, result.Y);
            Assert.AreEqual(100, result.X);
            Assert.AreEqual(TimeSpan.Zero, result.TimeStamp);
        }

        [Test]
        public void Convert_Position_Out_Of_Range()
        {
            var position = new Position { TimeStamp = TimeSpan.Zero, X = 1, Y = 16 };
            var ground = new Ground() { Width = 2800, Height = 1500 };
            var converter = new VisualDataConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(-100, result.Y);
            Assert.AreEqual(100, result.X);
            Assert.AreEqual(TimeSpan.Zero, result.TimeStamp);
        }
    }
}
