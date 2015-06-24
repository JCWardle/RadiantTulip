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
    public class BindingCombinerTests
    {
        [Test]
        public void Joins_Objects_Together()
        {
            var obj1 = 1;
            var obj2 = 2;
            var converter = new BindingCombiner();

            var result = (object[])converter.Convert(new object[] { obj1, obj2 }, null, null, null);

            Assert.AreEqual(result[0], 1);
            Assert.AreEqual(result[1], 2);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Binding_Combiner_Convert_Back_Not_Implemented()
        {
            var converter = new BindingCombiner();
            converter.ConvertBack(null, null, null, null);
        }
    }
}
