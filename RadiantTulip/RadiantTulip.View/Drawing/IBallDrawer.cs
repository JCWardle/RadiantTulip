using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RadiantTulip.View.Drawing
{
    public interface IBallDrawer
    {
        void Draw(Canvas canvas, Ball ball, Player player, Ground ground, IReadOnlyDictionary<Size, int> scaleSettings);
    }
}
