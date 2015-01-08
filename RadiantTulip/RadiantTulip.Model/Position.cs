using System;

namespace RadiantTulip.Model
{
    public class Position
    {
        //Longitude or converted X co-ordinate
        public double X { get; set; }
        //Latitude or converted Y co-ordinate
        public double Y { get; set; }
        public TimeSpan TimeStamp { get; set; }
    }
}
