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
    public class MultipleSelectTests
    {
        [Test]
        public void Multiple_Select_Multiple_Players_Selected()
        {
            var converter = new MultipleSelect();

            var result = (Visibility)converter.Convert(SelectionState.MultiplePlayers, null, null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        [Test]
        public void Multiple_Select_No_Players_Selected()
        {
            var converter = new MultipleSelect();

            var result = (Visibility)converter.Convert(SelectionState.None, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        public void Multiple_Select_One_Player_Selected()
        {
            var converter = new MultipleSelect();

            var result = (Visibility)converter.Convert(SelectionState.SinglePlayer, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        public void Multiple_Select_Group_Selected()
        {
            var converter = new MultipleSelect();

            var result = (Visibility)converter.Convert(SelectionState.Group, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Multiple_Select_Convert_Back_Not_Implemented()
        {
            var converter = new MultipleSelect();
            converter.ConvertBack(null, null, null, null);
        }
    }
}
