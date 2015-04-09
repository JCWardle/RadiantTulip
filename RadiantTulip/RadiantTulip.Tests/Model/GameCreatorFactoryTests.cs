using NUnit.Framework;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class GameCreatorFactoryTests
    {
        [Test]
        public void Game_Creator_For_Excel_Extension()
        {
            var factory = new GameCreatorFactory();

            var result = factory.CreateGameCreator("blah.xlsx");

            Assert.NotNull(result);
        }

        [Test]
        public void Game_Creator_For_Old_Excel_Extension()
        {
            var factory = new GameCreatorFactory();

            var result = factory.CreateGameCreator("blah.xls");

            Assert.NotNull(result);
        }

        [Test]
        public void Game_Creator_For_Txt_Extension()
        {
            var factory = new GameCreatorFactory();

            var result = factory.CreateGameCreator("blah.txt");

            Assert.NotNull(result);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException), ExpectedMessage = "File type not supported")]
        public void Game_Creator_For_Unimplemented_Extension()
        {
            var factory = new GameCreatorFactory();

            var result = factory.CreateGameCreator("");
        }
    }
}
