using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameModel = RadiantTulip.Model.Game;

namespace RadiantTulip.View.Game
{
    public class GameDrawer : IGameDrawer
    {
        
        public GameDrawer(IGroundDrawer groundDrawer, IPlayerDrawer playerDrawer)
        {

        }

        public void DrawGame(Canvas canvas, DataGrid grid, GameModel game)
        {
            throw new NotImplementedException();
        }

        public void AddVisualArtifact(IVisualArtifact artifact)
        {
            throw new NotImplementedException();
        }

        public void AddDescriptiveArtifact(IDescriptiveArtifact artifact)
        {
            throw new NotImplementedException();
        }

        public void RemoveVisualArtifact(IVisualArtifact artifact)
        {
            throw new NotImplementedException();
        }

        public void RemoveDescriptiveArtifact(IDescriptiveArtifact artifact)
        {
            throw new NotImplementedException();
        }
    }
}
