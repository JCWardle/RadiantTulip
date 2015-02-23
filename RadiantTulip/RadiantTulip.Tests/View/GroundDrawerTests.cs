using System.Reflection;
using NUnit.Framework;
using System.IO;
using System.Windows.Controls;
using RadiantTulip.View.Game;
using RadiantTulip.Model;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace RadiantTulip.Tests.View
{
    [TestFixture, RequiresSTA]
    public class GroundDrawerTests
    {
        [Test]
        public void Draws_Ground_First_Time()
        {
            var image = GetImagePath("Patersons.png");
            var canvas = new Canvas();
            var ground = new Ground {Image = image};
            var drawer = new AFLGroundDrawer(ground);

            drawer.Draw(canvas);

            Assert.AreEqual("ImageBrush", canvas.Background.GetType().Name);
            var brush = (BitmapImage)((ImageBrush)canvas.Background).ImageSource;
            Assert.AreEqual(image, brush.UriSource.LocalPath);
        }

        private string GetImagePath(string imageName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestFiles", imageName);
        }
    }
}
