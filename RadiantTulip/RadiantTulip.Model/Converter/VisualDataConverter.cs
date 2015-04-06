using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Converter
{
    public class VisualDataConverter : ICoordinateConverter
    {
        public Position Convert(Position position, Ground ground)
        {
            return new Position 
            { 
                TimeStamp = position.TimeStamp, 
                X = position.X * 100,
                Y = ground.Height - position.Y * 100
            };
        }
    }
}
