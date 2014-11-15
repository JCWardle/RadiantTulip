using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class Gaame
    {
        public List<Team> Teams { get; set; }
        public GameState GameState { get; set; }
        public Ground Ground { get; set; }
    }
}
