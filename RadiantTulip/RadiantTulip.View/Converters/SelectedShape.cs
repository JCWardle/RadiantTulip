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
    public class SelectedShape : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<Player> players = new ObservableCollection<Player>();
            var state = (SelectionState)values[2];

            if (state == SelectionState.MultiplePlayers || state == SelectionState.SinglePlayer)
                players = (ObservableCollection<Player>)values[0];
            else if (state == SelectionState.Group && values[1] != null)
                players = ((Group)values[1]).Players;

            if (players.Count == 0)
                return PlayerShape.Circle;

            return players.First().Shape;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
