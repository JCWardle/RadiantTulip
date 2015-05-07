using RadiantTulip.Model;
using RadiantTulip.View.Game.VisualAffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using GameModel = RadiantTulip.Model.Game;

namespace RadiantTulip.View.Game
{
    public class GameDrawer : IGameDrawer
    {
        private GroundDrawer _groundDrawer;
        private IPlayerDrawer _playerDrawer;

        public GameDrawer(IGroundDrawerFactory groundFactory, IPlayerDrawer playerDrawer, Ground ground)
        {
            _groundDrawer = groundFactory.CreateGroundDrawer(ground);
            _playerDrawer = playerDrawer;
        }

        public void DrawGame(Canvas canvas, GameModel game, IList<IVisualAffect> visualAffects)
        {
            canvas.Children.Clear();
            canvas.Background = new SolidColorBrush(Colors.Green);

            foreach (var v in visualAffects)
                v.Draw(canvas);

            _groundDrawer.Draw(canvas);

            foreach (var t in game.Teams)
            {
                foreach (var p in t.Players.Where(p => p.Visible))
                {
                    _playerDrawer.Draw(p, game.Ground, canvas);
                }
            }
        }
    }
}
