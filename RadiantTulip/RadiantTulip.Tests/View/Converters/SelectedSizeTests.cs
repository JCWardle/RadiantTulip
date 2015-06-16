using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class SelectedSizeTests
    {
        [Test]
        public void Selected_Size_One_Player_Selected()
        {
            var player = new Player { Size = Size.Medium };
            var converter = new SelectedSize();
            var players = new ObservableCollection<Player>() { player };
            var parameters = new object[] { players, null, SelectionState.SinglePlayer };

            var result = (int)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(Size.Medium, result);
        }

        [Test]
        public void Selected_Size_Two_Players_Selected()
        {
            var converter = new SelectedSize();
            var players = new ObservableCollection<Player>() 
            { 
                new Player { Size = Size.Large },
                new Player { Size = Size.Large }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (int)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(Size.Large, result);
        }

        [Test]
        public void Selected_Size_Two_Players_Different_Selected()
        {
            var converter = new SelectedSize();
            var players = new ObservableCollection<Player>() 
            { 
                new Player { Size = Size.Large },
                new Player { Size = Size.Medium }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (int)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(Size.Large, result);
        }

        [Test]
        public void Selected_Size_Group_Selected()
        {
            var converter = new SelectedSize();
            var group = new Group
            {
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Size = Size.ExtraLarge },
                    new Player { Size = Size.ExtraLarge }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (int)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(Size.ExtraLarge, result);
        }

        [Test]
        public void Selected_Size_Group_Different_Selected()
        {
            var converter = new SelectedSize();
            var group = new Group
            {
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Size = Size.ExtraLarge },
                    new Player { Size = Size.Medium }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (int)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(Size.ExtraLarge, result);
        }

        [Test]
        public void Selected_Size_Nothing_Selected()
        {
            var converter = new SelectedSize();
            var parameters = new object[] { null, null, SelectionState.None };

            var result = (int)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(Size.Medium, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Selected_Size_Convert_Back_Not_Implemented()
        {
            var converter = new SelectedSize();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
