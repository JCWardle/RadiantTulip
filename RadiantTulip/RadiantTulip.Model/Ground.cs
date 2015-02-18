namespace RadiantTulip.Model
{
    public class Ground
    {
        public string Image { get; set; }
        public double CentreLongitude { get; set; }
        public double CentreLatitude { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Padding { get; set; }
        public GroundType Type { get; set; }
    }
}
