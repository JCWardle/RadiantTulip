using RadiantTulip.Model;
using System.Collections.Generic;
using System.Windows.Controls;

namespace RadiantTulip.View.Drawing
{
    public interface IPlayerDrawer
    {
        void Draw(Player player, Ground ground, Canvas canvas, IReadOnlyDictionary<Size, int> scaleSettings);
    }
}
