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
    public class AffectSelected : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var players = (ObservableCollection<Player>)values[0];
            var affect = values[1];
            var affects = (List<IVisualAffect>)values[2];

            if(affect.GetType().IsAssignableFrom(typeof (PlayerAffect)))
            {
                return affects.Any(a => a.AffectFor(players.ToList(), (PlayerAffect)affect));
            }
            else
            {
                return affects.Any(a => a.AffectFor(players.ToList(), (GroupAffect)affect));
            }            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
