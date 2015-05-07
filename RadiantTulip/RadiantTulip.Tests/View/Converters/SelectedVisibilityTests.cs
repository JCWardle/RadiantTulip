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
    public class SelectedVisibilityTests
    {
        [Test]
        public void Is_Visible_One_Player_Selected()
        {
            var player = new Player { Visible = true };
            var converter = new SelectedVisibility();
            var players = new ObservableCollection<Player>() { player };
            var parameters = new object[] { players, null, SelectionState.SinglePlayer };

            var result = (bool?)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Is_Visible_Two_Players_Selected()
        {
            var converter = new SelectedVisibility();
            var players = new ObservableCollection<Player>() 
            { 
                new Player { Visible = true },
                new Player { Visible = true }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (bool?)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Is_Visible_Two_Players_Different_Selected()
        {
            var converter = new SelectedVisibility();
            var players = new ObservableCollection<Player>() 
            { 
                new Player { Visible = true },
                new Player { Visible = false }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (bool?)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Is_Visible_Group_Selected()
        {
            var converter = new SelectedVisibility();
            var group = new Group
            {
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Visible = true },
                    new Player { Visible = true }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (bool?)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Is_Visible_Group_Different_Selected()
        {
            var converter = new SelectedSize();
            var group = new Group
            {
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Visible = true },
                    new Player { Visible = false }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (bool?)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result.Value);
        }

        [Test]
        public void Is_Visible_Nothing_Selected()
        {
            var converter = new SelectedSize();
            var parameters = new object[] { null, null, SelectionState.None };

            var result = (bool?)converter.Convert(parameters, null, null, null);

            Assert.IsNull(result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Is_Visible_Convert_Back_Not_Implemented()
        {
            var converter = new SelectedSize();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
