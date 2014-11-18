using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Converter
{
    public interface ICoordinateConverter
    {
        Position Convert(Position position, Ground ground);
    }
}
