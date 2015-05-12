using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class Game
    {
        public List<Team> Teams { get; set; }
        public Ball Ball { get; set; }
        public GameState GameState { get; set; }
        public Ground Ground { get; set; }
    }
}
