using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Game.VisualEffects
{
    public interface IAffectFactory
    {
        IVisualAffect CreatePlayerEffect(Player player, PlayerAffect effect, Model.Game game);
        IVisualAffect CreateGroupEffect(IList<Model.Player> player, GroupAffect effect, Model.Game game);
    }
}
