using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.Drawing
{
    public interface IGroundDrawerFactory
    {
        GroundDrawer CreateGroundDrawer(Ground ground);
    }
}
