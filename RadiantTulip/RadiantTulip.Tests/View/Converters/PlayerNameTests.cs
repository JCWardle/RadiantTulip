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
    public class PlayerNameTests
    {
        [Test]
        public void Gets_Player_Name_From_Selected_Players()
        {
            var player = new Player { Name = "PlayerA"};
            var parameter = new ObservableCollection<Player> { player };
            var converter = new PlayerName();

            var result = converter.Convert(parameter, null, null, null);

            Assert.AreEqual("PlayerA", result);
        }

        [Test]
        public void Gets_Player_Name_With_Null_Players()
        {
            var parameter = new ObservableCollection<Player>();
            var converter = new PlayerName();

            var result = converter.Convert(parameter, null, null, null);

            Assert.AreEqual("", result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Player_Name_Convert_Back_Not_Implemented()
        {
            var converter = new PlayerName();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
