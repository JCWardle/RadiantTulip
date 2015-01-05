using System.Device.Location;

namespace RadiantTulip.Model.Converter
{
    public class GPSConverter : ICoordinateConverter
    {
        public Position Convert(Position position, Ground ground)
        {
            var groundCenter = new GeoCoordinate(ground.CentreLatitude, ground.CentreLongitude);
            var positionX = new GeoCoordinate(position.X, ground.CentreLongitude);
            var positionY = new GeoCoordinate(ground.CentreLatitude, position.Y);

            //GetDistanceTo returns the value in meters, centimeters are required
            var xDistance = groundCenter.GetDistanceTo(positionX) * 100;
            var yDistance = groundCenter.GetDistanceTo(positionY) * 100;

            //If the player is on the right hand side of the ground place the co-ordinates over there
            if (position.X < ground.CentreLatitude)
                xDistance += ground.Width / 2d;
            else
                xDistance = (ground.Width / 2d) - xDistance;

            if (position.Y > ground.CentreLongitude)
                yDistance += ground.Height / 2d;
            else
                yDistance = (ground.Height / 2d) - yDistance;

            return new Position
            {
                TimeStamp = position.TimeStamp,
                X = xDistance,
                Y = yDistance
            };
        }
    }
}
