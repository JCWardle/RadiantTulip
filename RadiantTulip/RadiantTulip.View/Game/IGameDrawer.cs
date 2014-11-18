using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using GameModel = RadiantTulip.Model.Game;

namespace RadiantTulip.View.Game
{
    public interface IGameDrawer
    {
        void DrawGame(Canvas canvas, Table table, GameModel game);
        void AddVisualArtifact(IVisualArtifact artifact);
        void AddDescriptiveArtifact(IDescriptiveArtifact artifact);
        void RemoveVisualArtifact(IVisualArtifact artifact);
        void RemoveDescriptiveArtifact(IDescriptiveArtifact artifact);
    }
}