using NUnit.Framework;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class JsonGroundReaderTests
    {
        [Test]
        public void Read_One_Ground()
        {
            var reader = new JsonGroundReader();
            IList<Ground> result;
            using (var stream = GetFilePath("Ground1.json"))
            {
                var list = new List<Stream>() { stream };
                result = reader.ReadGrounds(list);
            }

            Assert.AreEqual(1, result.Count);
            var ground = result.First();
            Assert.AreEqual(@"E:\Code\RadiantTulip\RadiantTulip\Grounds\Patersons.png", ground.Image);
            Assert.AreEqual(-31.944664, ground.CentreLatitude);
            Assert.AreEqual(115.830156, ground.CentreLongitude);
            Assert.AreEqual(22100, ground.Height);
            Assert.AreEqual(17200, ground.Width);
            Assert.AreEqual(10, ground.Padding);
            Assert.AreEqual("Pattersons", ground.Name);
            Assert.AreEqual(GroundType.AFL, ground.Type);
        }

        [Test]
        public void Read_Two_Grounds()
        {
            var reader = new JsonGroundReader();
            IList<Ground> result;
            using (var stream1 = GetFilePath("Ground2.json"))
            using (var stream2 = GetFilePath("Ground1.json"))
            {
                var list = new List<Stream>() { stream1, stream2 };
                result = reader.ReadGrounds(list);
            }

            Assert.AreEqual(2, result.Count);
            var ground = result[1];
            Assert.AreEqual(@"E:\Code\RadiantTulip\RadiantTulip\Grounds\Patersons.png", ground.Image);
            Assert.AreEqual(-31.944664, ground.CentreLatitude);
            Assert.AreEqual(115.830156, ground.CentreLongitude);
            Assert.AreEqual(22100, ground.Height);
            Assert.AreEqual(17200, ground.Width);
            Assert.AreEqual(10, ground.Padding);
            Assert.AreEqual("Pattersons", ground.Name);
            Assert.AreEqual(GroundType.AFL, ground.Type);

            ground = result[0];
            Assert.AreEqual(@"E:\Code\RadiantTulip\RadiantTulip\Grounds\MCG.png", ground.Image);
            Assert.AreEqual(-37.819963, ground.CentreLatitude);
            Assert.AreEqual(144.983435, ground.CentreLongitude);
            Assert.AreEqual(22100, ground.Height);
            Assert.AreEqual(17200, ground.Width);
            Assert.AreEqual(10, ground.Padding);
            Assert.AreEqual("MCG", ground.Name);
            Assert.AreEqual(GroundType.AFL, ground.Type);
        }

        private Stream GetFilePath(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Concat("RadiantTulip.Tests.TestFiles.", fileName));
        }
    }
}
