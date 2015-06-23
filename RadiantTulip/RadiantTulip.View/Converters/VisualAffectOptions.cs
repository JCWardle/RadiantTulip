using RadiantTulip.Model;
using RadiantTulip.View.Drawing.VisualAffects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RadiantTulip.View.Converters
{
    public class VisualAffectOptions : IMultiValueConverter
    {
        /// <summary>
        /// Finds the visual affect for a given player and presents returns a list of strings for the options for the affact
        /// </summary>
        /// <param name="values">SelectedPlayers, Visual Affect Enum Value, List of visual affects currently being used, The count of selected players</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var players = (ObservableCollection<Player>)values[0];
            var affect = values[1];
            var affects = (List<IVisualAffect>)values[2];

            IVisualAffect result;

            if (affect.GetType().IsAssignableFrom(typeof(PlayerAffect)))
            {
                result = affects.FirstOrDefault(a => a.AffectFor(players.ToList(), (PlayerAffect)affect));
            }
            else
            {
                result = affects.FirstOrDefault(a => a.AffectFor(players.ToList(), (GroupAffect)affect));
            }

            return result == null ? new List<string>() : result.Options();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
