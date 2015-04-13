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
    public class SelectedShapeTests
    {
        [Test]
        public void Selected_Shape_One_Player_Selected()
        {
            var player = new Player { Shape = PlayerShape.Circle };
            var converter = new SelectedShape();
            var players = new ObservableCollection<Player>() { player };
            var parameters = new object[] { players, null, SelectionState.SinglePlayer };

            var result = (PlayerShape)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(PlayerShape.Circle, result);
        }

        [Test]
        public void Selected_Shape_Two_Players_Selected()
        {
            var converter = new SelectedShape();
            var players = new ObservableCollection<Player>() 
            { 
                    new Player { Shape = PlayerShape.Square },
                    new Player { Shape = PlayerShape.Square }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (PlayerShape)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(PlayerShape.Square, result);
        }

        [Test]
        public void Selected_Shape_Two_Players_Different_Selected()
        {
            var converter = new SelectedShape();
            var players = new ObservableCollection<Player>() 
            { 
                    new Player { Shape = PlayerShape.Square },
                    new Player { Shape = PlayerShape.Circle }
            };
            var parameters = new object[] { players, null, SelectionState.MultiplePlayers };

            var result = (PlayerShape)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(PlayerShape.Square, result);
        }

        [Test]
        public void Selected_Shape_Group_Selected()
        {
            var converter = new SelectedShape();
            var group = new Group
            {
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Shape = PlayerShape.Square },
                    new Player { Shape = PlayerShape.Square }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (PlayerShape)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(PlayerShape.Square, result);
        }

        [Test]
        public void Selected_Shape_Group_Different_Selected()
        {
            var converter = new SelectedShape();
            var group = new Group
            {
                Players = new ObservableCollection<Player>() 
                { 
                    new Player { Shape = PlayerShape.Square },
                    new Player { Shape = PlayerShape.Circle }
                }
            };
            var parameters = new object[] { null, group, SelectionState.Group };

            var result = (PlayerShape)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(PlayerShape.Square, result);
        }

        [Test]
        public void Selected_Shape_Nothing_Selected()
        {
            var converter = new SelectedShape();
            var parameters = new object[] { null, null, SelectionState.None };

            var result = (PlayerShape)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(PlayerShape.Circle, result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Selected_Shape_Convert_Back_Not_Implemented()
        {
            var converter = new SelectedShape();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
