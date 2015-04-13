using NUnit.Framework;
using RadiantTulip.Model;
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
    public class IsSelectedTests
    {
        [Test]
        public void Is_Selected_One_Player()
        {
            var converter = new IsSelected();
            var player = new Player();
            var players = new ObservableCollection<Player>() { player };
            var parameters = new object[] {player, players};

            var result = (bool)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Is_Selected_Two_Players()
        {
            var converter = new IsSelected();
            var player = new Player();
            var players = new ObservableCollection<Player>() { player, new Player() };
            var parameters = new object[] { player, players };

            var result = (bool)converter.Convert(parameters, null, null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Is_Selected_Player_Not_In_List()
        {
            var converter = new IsSelected();
            var player = new Player();
            var players = new ObservableCollection<Player>() { new Player(), new Player() };
            var parameters = new object[] { player, players };

            var result = (bool)converter.Convert(parameters, null, null, null);

            Assert.IsFalse(result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Is_Selected_ConvertBack_Not_Implemented()
        {
            var converter = new IsSelected();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
