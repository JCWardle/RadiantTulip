using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RadiantTulip.View.Game
{
    public interface IVisualArtifact
    {
        void Draw(Canvas canvas, Player Player);
    }
}
