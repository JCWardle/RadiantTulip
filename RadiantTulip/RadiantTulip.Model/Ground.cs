namespace RadiantTulip.Model
{
    public class Ground
    {
        public double CentreLongitude { get; set; }
        public double CentreLatitude { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Padding { get; set; }
        public GroundType Type { get; set; }
        public string Name { get; set; }
        public float Rotation { get; set; }
    }
}
