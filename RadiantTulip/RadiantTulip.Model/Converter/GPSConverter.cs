using System;
using System.Device.Location;

namespace RadiantTulip.Model.Converter
{
    public class GPSConverter : ICoordinateConverter
    {
        public Position Convert(Position position, Ground ground)
        {
            var groundCenter = new GeoCoordinate(ground.CentreLatitude, ground.CentreLongitude);
            var positionX = new GeoCoordinate(ground.CentreLatitude, position.X);
            var positionY = new GeoCoordinate(position.Y, ground.CentreLongitude);

            //GetDistanceTo returns the value in meters, centimeters are required
            var xDistance = groundCenter.GetDistanceTo(positionX) * 100;
            var yDistance = groundCenter.GetDistanceTo(positionY) * 100;

            if (position.X < ground.CentreLongitude)
                xDistance = ground.Width / 2d - xDistance;
            else
                xDistance = (ground.Width / 2d) + xDistance;

            if (position.Y > ground.CentreLatitude)
                yDistance = ground.Height / 2d - yDistance;
            else
                yDistance = (ground.Height / 2d) + yDistance;

            var theta = ConvertDegreesToRadians(ground.Rotation);
            var originalX = xDistance;

            /*xDistance -= ground.Width / 2;
            yDistance -= ground.Height / 2;

            xDistance = (xDistance * Math.Cos(theta)) - (yDistance * Math.Sin(theta));
            yDistance = (originalX * Math.Sin(theta)) + (yDistance * Math.Cos(theta));

            xDistance += ground.Width / 2;
            yDistance += ground.Height / 2;*/

            return new Position
            {
                TimeStamp = position.TimeStamp,
                X = xDistance,
                Y = yDistance
            };
        }

        private double ConvertDegreesToRadians(float degrees)
        {
            return (Math.PI / 180) * degrees;
        }
    }
}
