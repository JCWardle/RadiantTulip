using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RadiantTulip.View.Converters
{
    public class IsSelected : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(values[0] == BindingOperations.DisconnectedSource)
                return false;

            var player = (Player)values[0];
            var collection = (ObservableCollection<Player>)values[1];

            return collection.Contains(player);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
