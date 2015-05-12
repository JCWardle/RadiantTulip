using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RadiantTulip.Model
{
    public class Ball
    {
        public Color Colour { get; set; }
        public IList<Position> Positions { get; set; }
        public Position CurrentPosition { get; set; }
    }
}
