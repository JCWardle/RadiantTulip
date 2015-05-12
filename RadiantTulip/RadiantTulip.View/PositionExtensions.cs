using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RadiantTulip.View
{
    public static class PositionExtensions
    {
        public static Point TransformToCanvas(this Position pos, Ground ground, Canvas canvas)
        {
            var xpos = (pos.X + ground.Padding) / (ground.Width + ground.Padding * 2) * canvas.ActualWidth;
            var ypos = (pos.Y + ground.Padding) / (ground.Height + ground.Padding * 2) * canvas.ActualHeight;

            return new Point() { X = xpos, Y = ypos };
        }

        public static double DistanceTo(this Position pos1, Position pos2)
        {
            return Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }
    }
}
