using RadiantTulip.Model;
using System.Windows.Controls;

namespace RadiantTulip.View.Game
{
    public interface IPlayerDrawer
    {
        void Draw(Player player, Ground ground, Canvas canvas);
    }
}
