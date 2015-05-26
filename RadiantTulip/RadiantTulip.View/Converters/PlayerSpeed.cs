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
    public class PlayerSpeed : IMultiValueConverter
    {
        /// <summary>
        /// Calculates a players speed and returns it in m/s
        /// </summary>
        /// <param name="values">First item is the player
        /// Second item is a timespan with the interval that the speed should be calculated over
        /// </param>
        /// <param name="targetType">null</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var player = ((ObservableCollection<Player>)values[0]).FirstOrDefault();
            var interval = (TimeSpan)values[1];

            if (player == null)
                return 0d;

            var positions = player.Positions.Where(p => p.TimeStamp <= player.CurrentPosition.Value.TimeStamp
                && player.CurrentPosition.Value.TimeStamp - p.TimeStamp <= interval).ToList();

            var distance = 0d;

            for (var i = 1; i < positions.Count; i++)
            {
                distance += positions[i].DistanceTo(positions[i - 1]);
            }

            var speed = distance / interval.TotalMilliseconds;

            //Convert speed from centimetres / millisecond to metres / second
            return Math.Round(speed * 10, 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
