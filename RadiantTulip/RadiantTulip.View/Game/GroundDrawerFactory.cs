using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Game
{
    public class GroundDrawerFactory : IGroundDrawerFactory
    {
        public IGroundDrawer CreateGroundDrawer(Ground ground)
        {
            switch(ground.Type)
            {
                case GroundType.AFL:
                    return new AFLGroundDrawer(ground);
            }


            throw new NotImplementedException();
        }
    }
}
