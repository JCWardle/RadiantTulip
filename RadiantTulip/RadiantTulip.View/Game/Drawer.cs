using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RadiantTulip.View.Game
{
    public abstract class Drawer
    {
        /// <summary>
        /// Converts a players position in centimeters to the canvas's position.
        /// </summary>
        /// <param name="x">The players x position on the ground</param>
        /// <param name="y">The players y position on the ground</param>
        /// <param name="ground">The ground that player is on</param>
        /// <param name="canvas">The canvas that it is going to be drawn to</param>
        /// <returns>Returns x, y</returns>
        public Tuple<double, double> TransformToCanvas(double x, double y, Ground ground, Canvas canvas)
        {
            var xpos = x / ground.Width * canvas.ActualWidth;
            var ypos = y / ground.Height * canvas.ActualHeight;

            return new Tuple<double, double>(xpos, ypos);
        }
    }
}
