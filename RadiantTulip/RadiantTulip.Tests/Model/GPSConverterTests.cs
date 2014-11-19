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
            var position = new Position { X = -31.94453984, Y = 115.8308636 };
            var ground = new Ground();
            var converter = new GPSConverter();

            var result = converter.Convert(position, ground);


        }
    }
}
