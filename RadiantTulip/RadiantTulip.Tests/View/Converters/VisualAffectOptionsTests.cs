using NUnit.Framework;
using RadiantTulip.Model;
using RadiantTulip.View.Converters;
using RadiantTulip.View.Drawing.VisualAffects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    class VisualAffectOptionsTests
    {
        [Test]
        public void Get_Player_Affect_Options_One_Player_Selected()
        {
            var converter = new VisualAffectOptions();
            var player = new Player();
            var players = new ObservableCollection<Player>() { player };
            var ground = new Ground();

            var affects = new List<IVisualAffect>()
            {
                new Tadpole(player, ground)
            };
            var parameters = new object[] { players, PlayerAffect.Tadpole, affects };

            var result = (IList<string>)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Small", result[0]);
            Assert.AreEqual("Medium", result[1]);
            Assert.AreEqual("Large", result[2]);
        }

        [Test]
        public void Get_Player_Affect_Options_Two_Players_Selected_Same_Affect_Type()
        {
            var converter = new VisualAffectOptions();
            var player = new Player();
            var player1 = new Player();
            var players = new ObservableCollection<Player>() { player, player1 };
            var ground = new Ground();

            var affects = new List<IVisualAffect>()
            {
                new Tadpole(player, ground),
                new Tadpole(player1, ground),
            };
            var parameters = new object[] { players, PlayerAffect.Tadpole, affects };

            var result = (IList<string>)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Small", result[0]);
            Assert.AreEqual("Medium", result[1]);
            Assert.AreEqual("Large", result[2]);
        }

        [Test]
        public void Get_Player_Affect_No_Matching_Affect_Returns_Empty_List()
        {
            var converter = new VisualAffectOptions();
            var player = new Player();
            var players = new ObservableCollection<Player>() { player };
            var ground = new Ground();

            var affects = new List<IVisualAffect>();

            var parameters = new object[] { players, PlayerAffect.Tadpole, affects };

            var result = (IList<string>)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Convert_Back_Affect_Selected()
        {
            var converter = new VisualAffectOptions();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
