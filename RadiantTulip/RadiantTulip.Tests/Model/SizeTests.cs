using NUnit.Framework;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class SizeTests
    {
        [Test]
        public void Sizes_Initialise_Correctly()
        {
            Size.SetupSizes(1, 2, 3, 4);

            Assert.AreEqual(1, Size.Small);
            Assert.AreEqual(2, Size.Medium);
            Assert.AreEqual(3, Size.Large);
            Assert.AreEqual(4, Size.ExtraLarge);
        }

        [Test]
        public void Sizes_Default()
        {
            Size.SetupSizes();

            Assert.AreEqual(20, Size.Small);
            Assert.AreEqual(30, Size.Medium);
            Assert.AreEqual(40, Size.Large);
            Assert.AreEqual(50, Size.ExtraLarge);
        }

        [Test]
        public void Gets_All_Sizing_Options()
        {
            Size.SetupSizes();

            var sizes = Size.GetOptions();

            Assert.AreEqual(sizes, 4);
            Assert.AreEqual("Small", sizes[0]);
            Assert.AreEqual("Medium", sizes[1]);
            Assert.AreEqual("Large", sizes[2]);
            Assert.AreEqual("Extra Large", sizes[3]);
        }

        [Test]
        public void Lookup_Sizing()
        {
            Size.SetupSizes();

            Assert.AreEqual(20, Size.GetSize("Small"));
        }
    }
}
