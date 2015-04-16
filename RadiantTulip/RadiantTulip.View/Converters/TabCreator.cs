using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RadiantTulip.View.Converters
{
    public class TabCreator : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length < 3
                || values[0].GetType() != typeof(DataTemplate)
                || values[1].GetType() != typeof(DataTemplate)
                || values[2].GetType() != typeof(TabControl))
                throw new ArgumentException();

            return new Tuple<DataTemplate, DataTemplate, TabControl>
            (
                item1: (DataTemplate)values[0],
                item2: (DataTemplate)values[1],
                item3: (TabControl)values[2]
            );
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
