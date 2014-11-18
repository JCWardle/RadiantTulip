using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class Player
    {
        public Team Team { get; set; }
        public bool Visible { get; set; }
        public List<Position> Positions { get; set; }
        public Position CurrentPosition { get; set; }
    }
}
