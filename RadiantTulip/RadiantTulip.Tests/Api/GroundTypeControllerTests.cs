using NUnit.Framework;
using RadiantTulip.API;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Api
{
    [TestFixture]
    public class GroundTypeControllerTests
    {
        [Test]
        public void Ground_Type_Controller_Get_All_Grounds()
        {
            var controller = new GroundTypeController();

            var result = controller.Get();

            Assert.AreEqual(Enum.GetValues(typeof(GroundType)).Length, result.Count());
        }
    }
}
