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
            var ground = new Ground() { CentreLatitude = 389424.61, CentreLongitude = 6465102.83 };
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);

            Assert.AreEqual(2.2769579348387197, result.X);
            Assert.AreEqual(14.209707674570382, result.Y);
            Assert.AreEqual(new DateTime(1, 1, 1), new DateTime(1, 1, 1));
        }
    }
}
