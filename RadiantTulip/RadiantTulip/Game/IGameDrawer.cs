using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RadiantTulip.View.Game
{
    public interface IGameDrawer
    {
        void DrawGame(Canvas canvas, DataGrid grid);
        void AddVisualArtifact(IVisualArtifact artifact);
        void AddDescriptiveArtifact(IDescriptiveArtifact artifact);
        void RemoveVisualArtifact(IVisualArtifact artifact);
        void RemoveDescriptiveArtifact(IDescriptiveArtifact artifact);
    }
}