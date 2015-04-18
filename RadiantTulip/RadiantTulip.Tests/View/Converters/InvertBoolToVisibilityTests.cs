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
    public class InvertBoolToVisibilityTests
    {
        [Test]
        public void Converts_True_To_Collapse()
        {
            var converter = new InvertBoolToVisibility();

            var result = converter.Convert(true, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        public void Converts_False_To_Collapse()
        {
            var converter = new InvertBoolToVisibility();

            var result = converter.Convert(true, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Invert_Bool_To_Visibility_Convert_Back_Not_Implemented()
        {
            var converter = new InvertBoolToVisibility();

            converter.ConvertBack(true, null, null, null);
        }
    }
}
