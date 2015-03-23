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
        public GroundDrawer CreateGroundDrawer(Ground ground)
        {
            switch(ground.Type)
            {
                case GroundType.AFL:
                    return new AFLGroundDrawer(ground);
                case GroundType.WheelChairRugby:
                    return new WheelChairRugbyGroundDrawer(ground);
            }


            throw new NotImplementedException();
        }
    }
}
