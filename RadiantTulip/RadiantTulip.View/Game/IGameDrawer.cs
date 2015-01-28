using System.Windows.Controls;
using System.Windows.Documents;
using GameModel = RadiantTulip.Model.Game;

namespace RadiantTulip.View.Game
{
    public interface IGameDrawer
    {
        void DrawGame(Canvas canvas, GameModel game);
        void AddVisualArtifact(IVisualArtifact artifact);
        void RemoveVisualArtifact(IVisualArtifact artifact);
    }
}