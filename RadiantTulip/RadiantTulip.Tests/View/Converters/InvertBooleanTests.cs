using NUnit.Framework;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    public class InvertBooleanTests
    {
        [Test]
        public void Convert_Invert_Boolean_Value()
        {
            var converter = new InvertBoolean();
            var value = false;

            var result = (bool)converter.Convert(value, typeof(bool), null, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Convert_Back_Invert_Boolean_Value()
        {
            var converter = new InvertBoolean();
            var value = true;

            var result = (bool)converter.Convert(value, typeof(bool), null, null);

            Assert.IsFalse(result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_Invert_Boolean_Invalid_Boolean()
        {
            var converter = new InvertBoolean();
            var value = new object();

            var result = (bool)converter.Convert(value, typeof(bool), null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_Back_Invert_Boolean_Invalid_Boolean()
        {
            var converter = new InvertBoolean();
            var value = new object();

            var result = (bool)converter.Convert(value, typeof(bool), null, null);
        }
    }
}
