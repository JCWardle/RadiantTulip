using RadiantTulip.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace RadiantTulip.View
{
    public class Group
    {
        public ObservableCollection<Player> Players { get; set; }
        public string Name { get; set; }
        /*
        private void blak ()
        {
            SystemColors.InactiveSelectionHighlightBrushKey
        }*/
    }
}
