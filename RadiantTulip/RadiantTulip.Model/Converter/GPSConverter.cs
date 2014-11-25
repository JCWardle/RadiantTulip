using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Converter
{
    public class GPSConverter : ICoordinateConverter
    {
        private const int EQUATORIAL_RADIUS = 6378160;
        private const double ECCENTRICITY = 0.081819191;
        private const double SCALE_FACTOR = 0.9996;
        private const double E1_SQUARE = 0.006739497;

        //Meridional Arc Constants
        private const double A0 = 6367449.1;
        private const double B0 = 16038.43;
        private const double C0 = 16.832613;
        private const double D0 = 0.0219844;
        private const double E0 = 0.000312705;

        public Position Convert(Position position, Ground ground)
        {
            var longitudeZone = 31 + Math.Round((position.Y / 6), 0);
            var longitudeZoneCM = 6 * longitudeZone - 183;
            var deltaLongitudeRad = (position.Y - longitudeZoneCM) * Math.PI / 180;
            var latitudeRad = position.X * Math.PI / 180;
            var longitudeRad = position.Y * Math.PI / 180;

            var rcurv1 = EQUATORIAL_RADIUS * (1 - ECCENTRICITY * ECCENTRICITY)
                /
                Math.Pow((1 - Math.Pow(ECCENTRICITY * Math.Sin(latitudeRad), 2)), 1.5);

            var rcurv2 = EQUATORIAL_RADIUS
                /
                Math.Pow(1 - (Math.Pow(ECCENTRICITY * Math.Sin(latitudeRad), 2)), .5);

            var meridionalArcS = A0 * latitudeRad
                - B0 * Math.Sin(2 * latitudeRad)
                + C0 * Math.Sin(4 * latitudeRad)
                - D0 * Math.Sin(6 * latitudeRad)
                + E0 * Math.Sin(8 * latitudeRad);

            var ki = meridionalArcS * SCALE_FACTOR;
            var kii = rcurv2 * Math.Sin(latitudeRad) * Math.Cos(latitudeRad) / 2;
            var kiii = (rcurv2 * Math.Sin(latitudeRad) * Math.Pow(Math.Cos(latitudeRad), 3) / 24)
                * (5 - Math.Pow(Math.Tan(latitudeRad), 2) + 9 * E1_SQUARE * Math.Pow(Math.Cos(latitudeRad), 2)
                + 4 * Math.Pow(E1_SQUARE, 2) * Math.Pow(Math.Cos(latitudeRad), 4)) * SCALE_FACTOR;

            var kiv = rcurv2 * Math.Cos(latitudeRad) * SCALE_FACTOR;
            var kv = Math.Pow(Math.Cos(latitudeRad), 3) * (rcurv2 / 6) * (1 - Math.Pow(Math.Tan(latitudeRad), 2)) 
                + E1_SQUARE * Math.Pow(Math.Cos(latitudeRad), 2) * SCALE_FACTOR;

            var ag = (Math.Pow(deltaLongitudeRad, 6) * rcurv2 * Math.Sin(latitudeRad) * Math.Pow(Math.Cos(latitudeRad), 5 / 720)
                * (61 - 58 * Math.Pow(Math.Tan(latitudeRad), 2) + Math.Pow(Math.Tan(latitudeRad), 4) + 270 * E1_SQUARE
                    * Math.Pow(Math.Cos(latitudeRad), 2) - 330 * E1_SQUARE * Math.Pow(Math.Sin(latitudeRad), 2))) * SCALE_FACTOR;

            var rawNorthing = (ki + kii * deltaLongitudeRad * deltaLongitudeRad + kiii * Math.Pow(deltaLongitudeRad, 4));

            var northing = rawNorthing < 0 ? 10000000 + rawNorthing : rawNorthing;
            var easting = 500000 + (kiv * deltaLongitudeRad + kv * Math.Pow(deltaLongitudeRad, 3));


            return new Position
            {
                TimeStamp = position.TimeStamp,
                X = easting - ground.CentreLatitude,
                Y = northing - ground.CentreLongitude
            };
        }
    }
}
