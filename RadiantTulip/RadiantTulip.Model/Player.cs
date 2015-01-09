using System.Collections.Generic;

namespace RadiantTulip.Model
{
    public class Player
    {
        public Team Team { get; set; }
        public bool Visible { get; set; }
        public List<Position> Positions { get; set; }
        public Position CurrentPosition { get; set; }
        public string Name { get; set; }
    }
}
