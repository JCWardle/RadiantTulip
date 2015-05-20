using NUnit.Framework;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RadiantTulip.Tests.Model
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Colour_Set_Notification()
        {
            var player = new Player() { Colour = Color.FromArgb(255, 255, 255, 255) };
            var eventTriggered = false;

            player.PropertyChanged +=
                delegate(object sender, PropertyChangedEventArgs e)
                {
                    eventTriggered = true;
                    Assert.AreEqual("Colour", e.PropertyName);
                };

            player.Colour = Color.FromArgb(0, 0, 0, 0);

            Assert.IsTrue(eventTriggered);
        }

        [Test]
        public void Visible_Set_Notification()
        {
            var player = new Player() { Visible = false };
            var eventTriggered = false;

            player.PropertyChanged +=
                delegate(object sender, PropertyChangedEventArgs e)
                {
                    eventTriggered = true;
                    Assert.AreEqual("Visible", e.PropertyName);
                };

            player.Visible = true;

            Assert.IsTrue(eventTriggered);
        }
    }
}
