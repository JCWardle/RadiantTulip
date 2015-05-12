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
    public class PlayerDistance : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var player = ((ObservableCollection<Player>)values[0]).FirstOrDefault();
            var time = TimeSpan.FromMilliseconds((double)values[1]);
            var distance = 0d;

            if (player == null)
                return distance;

            var positions = player.Positions.Where(p => p.TimeStamp <= time).OrderBy(p => p.TimeStamp).ToList();
            
            for(var i = 1; i < positions.Count; i++)
            {
                distance += positions[i].DistanceTo(positions[i - 1]); 
            }

            //Convert from cm to metres
            return Math.Round(distance / 100, 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
