using RadiantTulip.Model;
using System.Collections.ObjectModel;

namespace RadiantTulip.View
{
    public class Group
    {
        public ObservableCollection<Player> Players { get; set; }
        public string Name { get; set; }
    }
}
