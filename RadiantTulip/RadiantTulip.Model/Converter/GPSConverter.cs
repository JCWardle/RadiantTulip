using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Converter
{
    public class GPSConverter : ICoordinateConverter
    {
        public Position Convert(Position position, Ground ground)
        {
            var groundCenter = new GeoCoordinate(ground.CentreLatitude, ground.CentreLongitude);
            var positionX = new GeoCoordinate(position.X, ground.CentreLongitude);
            var positionY = new GeoCoordinate(ground.CentreLatitude, position.Y);

            return new Position
            {
                TimeStamp = position.TimeStamp,
                X = groundCenter.GetDistanceTo(positionX),
                Y = groundCenter.GetDistanceTo(positionY)
            };
        }
    }
}
