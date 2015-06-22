using RadiantTulip.View.Drawing.VisualAffects;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using GameModel = RadiantTulip.Model.Game;

namespace RadiantTulip.View.Drawing
{
    public interface IGameDrawer
    {
        void DrawGame(Canvas canvas, GameModel game, IList<IVisualAffect> visualAffects);
    }
}