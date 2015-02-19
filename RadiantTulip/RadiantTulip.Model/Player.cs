using System.Collections.Generic;
using System.Windows.Media;

namespace RadiantTulip.Model
{
    public class Player
    {
        public Team Team { get; set; }
        public bool Visible { get; set; }
        public List<Position> Positions { get; set; }
        public Position CurrentPosition { get; set; }
        public string Name { get; set; }
        public Color Colour { get; set; }
        public Size Size { get; set; }
        public PlayerShape Shape { get; set; }
    }
}
