using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Game.VisualAffects
{
    public class AffectFactory : IAffectFactory
    {
        public IVisualAffect CreatePlayerEffect(Model.Player player, PlayerAffect effect, Model.Game game)
        {
            switch(effect)
            {
                case PlayerAffect.Tadpole:
                    return new Tadpole(player, game.Ground);
            }

            return null;
        }

        public IVisualAffect CreateGroupEffect(IList<Model.Player> player, GroupAffect effect, Model.Game game)
        {
            throw new NotImplementedException();
        }
    }
}
