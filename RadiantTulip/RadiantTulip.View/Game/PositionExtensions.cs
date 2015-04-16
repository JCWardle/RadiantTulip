using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RadiantTulip.View.Game
{
    public static class PositionExtensions
    {
        public static Point TransformToCanvas(this Position pos, Ground ground, Canvas canvas)
        {
            var xpos = pos.X / (ground.Width + ground.Padding) * canvas.ActualWidth;
            var ypos = pos.Y / (ground.Height + ground.Padding) * canvas.ActualHeight;

            return new Point() { X = xpos, Y = ypos };
        }
    }
}
