using RadiantTulip.Model;
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
    public class GameDrawer : IGameDrawer
    {
        private IGroundDrawer _groundDrawer;
        private IPlayerDrawer _playerDrawer;
        private List<IVisualArtifact> _visualArtifacts;
        private List<IDescriptiveArtifact> _descriptiveArtifacts;

        public GameDrawer(IGroundDrawer groundDrawer, IPlayerDrawer playerDrawer)
        {
            _groundDrawer = groundDrawer;
            _playerDrawer = playerDrawer;
            _visualArtifacts = new List<IVisualArtifact>();
            _descriptiveArtifacts = new List<IDescriptiveArtifact>();
        }

        public void DrawGame(Canvas canvas, GameModel game)
        {
            canvas.Children.Clear();
            _groundDrawer.Draw(canvas, game.Ground);

            foreach(var t in game.Teams)
                foreach(var p in t.Players.Where(p => p.Visible))
                {
                    _playerDrawer.Draw(p, game.Ground, canvas);
                    ApplyVisualArtifacts(canvas, p);
                }
        }

        private void ApplyVisualArtifacts(Canvas canvas, Player player)
        {
            foreach(var v in _visualArtifacts)
                v.Draw(canvas, player);
        }

        public void AddVisualArtifact(IVisualArtifact artifact)
        {
            _visualArtifacts.Add(artifact);
        }

        public void RemoveVisualArtifact(IVisualArtifact artifact)
        {
            _visualArtifacts.Remove(artifact);
        }
    }
}
