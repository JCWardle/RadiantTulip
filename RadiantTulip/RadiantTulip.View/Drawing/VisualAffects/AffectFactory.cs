using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Drawing.VisualAffects
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

            throw new NotImplementedException();
        }

        public IVisualAffect CreateGroupEffect(IList<Model.Player> players, GroupAffect effect, Model.Game game)
        {
            switch(effect)
            {
                case GroupAffect.Lines:
                    return new Lines(players, game.Ground);
                case GroupAffect.OutterCoverage:
                    return new OutterCoverage(players, game.Ground);
            }

            throw new NotImplementedException();
        }
    }
}
