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
        /// <summary>
        /// Calculates the total distance the player has travelled for the game.
        /// It gets the distance from the player's current position.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var player = ((ObservableCollection<Player>)values[0]).FirstOrDefault();
            var distance = 0d;

            if (player == null || player.CurrentPosition == null)
                return distance;

            var previousPosition = player.CurrentPosition;

            while (previousPosition.Previous != null)
            {
                distance += previousPosition.Value.DistanceTo(previousPosition.Previous.Value);
                previousPosition = previousPosition.Previous;
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
