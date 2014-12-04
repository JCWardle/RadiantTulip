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
    public class GPSConverterTests
    {
        [Test]
        public void Test_Converter()
        {
            var position = new Position
            {
                X = -31.94451244,
                Y = 115.830105,
                TimeStamp = new DateTime(1, 1, 1)
            };
            var ground = new Ground() { CentreLatitude = -31.944464, CentreLongitude = 115.830156 };
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(5.3909321529616587, result.X);
            Assert.AreEqual(4.8162957775718658, result.Y);
            Assert.AreEqual(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
        }
    }
}
