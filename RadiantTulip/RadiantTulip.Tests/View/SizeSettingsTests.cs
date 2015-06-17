using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View
{
    [TestFixture]
    public class SizeSettingsTests
    {

        [Test]
        public void Read_ValidSize_Settings()
        {
            var stream = TestFileHelper.GetFilePath("ValidSettings.json");
            var sizeSettings = new SizeSettings();

            var result = sizeSettings.ReadSizeSettings(stream);

            Assert.AreEqual(50, result[Size.Small]);
            Assert.AreEqual(100, result[Size.Medium]);
            Assert.AreEqual(150, result[Size.Large]);
            Assert.AreEqual(200, result[Size.ExtraLarge]);
        }

        [Test]
        public void Read_Missing_Settings()
        {
            var stream = TestFileHelper.GetFilePath("MissingSettings.json");
            var sizeSettings = new SizeSettings();

            var result = sizeSettings.ReadSizeSettings(stream);

            Assert.AreEqual(50, result[Size.Small]);
            Assert.AreEqual(100, result[Size.Medium]);
            Assert.AreEqual(110, result[Size.Large]);
            Assert.AreEqual(120, result[Size.ExtraLarge]);
        }

        [Test]
        public void Settings_Default_With_No_File()
        {
            Stream stream = null;
            var sizeSettings = new SizeSettings();

            var result = sizeSettings.ReadSizeSettings(stream);

            Assert.AreEqual(10, result[Size.Small]);
            Assert.AreEqual(20, result[Size.Medium]);
            Assert.AreEqual(30, result[Size.Large]);
            Assert.AreEqual(40, result[Size.ExtraLarge]);
        }
    }
}
