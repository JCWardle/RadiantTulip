using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RadiantTulip.View.Game.VisualAffects
{
    public interface IVisualAffect
    {
        void Draw(Canvas canvas);
        bool AffectFor(List<Player> players, GroupAffect affect);
        bool AffectFor(List<Player> players, PlayerAffect affect);
    }
}
