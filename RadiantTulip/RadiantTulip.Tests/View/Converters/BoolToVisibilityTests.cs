using NUnit.Framework;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class BoolToVisibilityTests
    {
        [Test]
        public void Converts_True_To_Visible()
        {
            var converter = new BoolToVisibility();

            var result = (Visibility)converter.Convert(true, typeof(Visibility), null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        [Test]
        public void Converts_False_To_Collapsed()
        {
            var converter = new BoolToVisibility();

            var result = converter.Convert(false, typeof(Visibility), null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Visibility_Doesnt_Convert_Object()
        {
            var converter = new BoolToVisibility();

            var result = converter.Convert(new object(), typeof(Visibility), null, null);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Visibility_Convert_Back()
        {
            var converter = new BoolToVisibility();

            var result = converter.ConvertBack(false, typeof(Visibility), null, null);
        }
    }
}
