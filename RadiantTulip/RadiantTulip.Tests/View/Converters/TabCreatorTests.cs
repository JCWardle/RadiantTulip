using NUnit.Framework;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RadiantTulip.Tests.View.Converters
{
    [TestFixture]
    [RequiresSTA]
    public class TabCreatorTests
    {
        [Test]
        public void Creates_Tuple_Correctly()
        {
            var template1 = new DataTemplate();
            var template2 = new DataTemplate();
            var control = new TabControl();
            var converter = new TabCreator();
            var parameters = new object[] { template1, template2, control};

            var result = (Tuple<DataTemplate, DataTemplate, TabControl>)converter.Convert(parameters, null, null, null);

            Assert.AreEqual(template1, result.Item1);
            Assert.AreEqual(template2, result.Item2);
            Assert.AreEqual(control, result.Item3);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Not_Enough_Parameters()
        {
            var converter = new TabCreator();
            var parameters = new object[] { new DataTemplate(), new object(), new object() };
            
            converter.Convert(parameters, null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void No_Data_Template_Passed_In()
        {
            var converter = new TabCreator();
            var parameters = new object[] { new object(), new object(), new TabControl() };
            
            converter.Convert(parameters, null, null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void No_Tab_Control_Passed_In()
        {
            var converter = new TabCreator();
            var parameters = new object[] { new DataTemplate(), new DataTemplate(), new object() };
            
            converter.Convert(parameters, null, null, null);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Tab_Creator_Convert_Back_Not_Implemented()
        {
            var converter = new TabCreator();
            converter.ConvertBack(null, null, null, null);
        }
    }
}
