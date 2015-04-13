using NUnit.Framework;
using RadiantTulip.View;
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
    public class GroupSelectTests
    {
        [Test]
        public void Group_Selected_Shows_Context()
        {
            var converter = new GroupSelect();

            var result = converter.Convert(SelectionState.Group, typeof(Visibility), null, null);

            Assert.AreEqual(result, Visibility.Visible);
        }

        [Test]
        public void Group_Selected_Doesnt_Show_Context_When_Player_Selected()
        {
            var converter = new GroupSelect();

            var result = converter.Convert(SelectionState.SinglePlayer, typeof(Visibility), null, null);

            Assert.AreEqual(result, Visibility.Collapsed);
        }

        [Test]
        public void Group_Selected_Doesnt_Show_Context_When_Multiple_Players_Selected()
        {
            var converter = new GroupSelect();

            var result = converter.Convert(SelectionState.MultiplePlayers, typeof(Visibility), null, null);

            Assert.AreEqual(result, Visibility.Collapsed);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Group_Selected_Converter_Invalid_Argurment()
        {
            var converter = new GroupSelect();

            var result = converter.Convert(new object(), typeof(Visibility), null, null);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Group_Selected_Converter_Convert_Back_Not_Implemented()
        {
            var converter = new GroupSelect();

            var result = converter.ConvertBack(new object(), typeof(Visibility), null, null);
        }
    }
}
