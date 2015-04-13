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
using System.Windows.Media;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class SelectedColourTests
    {
        [Test]
        public void Selected_Colour_One_Player_Selected()
        {
            var player = new Player { Colour = new Color() { B = 255, G = 0, R = 0 } };
            var converter = new SelectedColour();
            var players = new ObservableCollection<Player>() {player};
            var parameters = new object[] { players, null, SelectionState.SinglePlayer };

            var result = (Color)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(new Color() { B = 255, G = 0, R = 0 }, result);
        }

        [Test]
        public void Selected_Colour_Two_Players_Selected()
        {
            var converter = new SelectedColour();
            var players = new ObservableCollection<Player>() 
            { 
                new Player { Colour = new Color() { B = 255, G = 0, R = 0 } },
                new Player { Colour = new Color() { B = 255, G = 0, R = 0 } }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (Color)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(new Color() { B = 255, G = 0, R = 0 }, result);
        }

        [Test]
        public void Selected_Colour_Group_Selected()
        {
            var converter = new SelectedColour();
            var group = new Group 
            { 
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Colour = new Color() { B = 255, G = 0, R = 0 } },
                    new Player { Colour = new Color() { B = 255, G = 0, R = 0 } }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (Color)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(new Color() { B = 255, G = 0, R = 0 }, result);
        }

        [Test]
        public void Selected_Colour_Nothing_Selected()
        {
            var converter = new SelectedColour();
            var parameters = new object[] { null, null, SelectionState.None };

            var result = (Color)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(new Color() { B = 0, G = 0, R = 255, A = 255 }, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Selected_Colour_Convert_Back_Not_Implemented()
        {
            var converter = new SelectedColour();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
