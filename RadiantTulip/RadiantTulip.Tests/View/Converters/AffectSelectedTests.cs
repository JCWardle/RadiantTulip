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
    public class AffectSelectedTests
    {
        [Test]
        public void Convert_Player_Affect_One_Player_Selected()
        {
            var converter = new AffectSelected();
            var player = new Player();
            var players = new ObservableCollection<Player>() { player };
            var ground = new Ground();

            var affects = new List<IVisualAffect>()
            {
                new Tadpole(player, ground)
            };
            var parameters = new object[] { players, PlayerAffect.Tadpole, affects };

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Convert_Player_Affect_Two_Players_Selected_Same_Affect_Type()
        {
            var converter = new AffectSelected();
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

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Convert_Player_Affect_No_Affects()
        {
            var converter = new AffectSelected();
            var player = new Player();
            var player1 = new Player();
            var players = new ObservableCollection<Player>() { player, player1 };
            var ground = new Ground();

            var affects = new List<IVisualAffect>();
            var parameters = new object[] { players, PlayerAffect.Tadpole, affects };

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Convert_Group_Affect_No_Affects()
        {
            var converter = new AffectSelected();
            var player = new Player();
            var player1 = new Player();
            var players = new ObservableCollection<Player>() { player, player1 };
            var ground = new Ground();

            var affects = new List<IVisualAffect>();
            var parameters = new object[] { players, GroupAffect.Lines, affects };

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Convert_Group_Affect_Two_Players_Selected_Different_Affect_Type()
        {
            var converter = new AffectSelected();
            var player = new Player();
            var player1 = new Player();
            var players = new ObservableCollection<Player>() { player, player1 };
            var ground = new Ground();

            var affects = new List<IVisualAffect>()
            {
                new Lines(players, ground),
                new OutterCoverage(players, ground)
            };
            var parameters = new object[] { players, GroupAffect.Lines, affects };

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Convert_Group_Affect_One_Player_Selected()
        {
            var converter = new AffectSelected();
            var player = new Player();
            var players = new ObservableCollection<Player>() { player };
            var ground = new Ground();

            var affects = new List<IVisualAffect>()
            {
                new Lines(players, ground)
            };
            var parameters = new object[] { players, GroupAffect.Lines, affects };

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Convert_Group_Affect_Two_Players_Selected_Same_Affect_Type()
        {
            var converter = new AffectSelected();
            var player = new Player();
            var player1 = new Player();
            var players = new ObservableCollection<Player>() { player, player1 };
            var ground = new Ground();

            var affects = new List<IVisualAffect>()
            {
                new Lines(players, ground),
                new Lines(new List<Player> { player1 }, ground),
            };
            var parameters = new object[] { players, GroupAffect.Lines, affects };

            var result = (bool)converter.Convert(parameters, typeof(bool), null, null);

            Assert.IsTrue(result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Convert_Back_Affect_Selected()
        {
            var converter = new AffectSelected();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
