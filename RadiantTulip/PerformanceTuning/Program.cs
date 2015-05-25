using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using RadiantTulip.View.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PerformanceTuning
{
    public class Position
    {
        //Longitude or converted X co-ordinate
        public double X { get; set; }
        //Latitude or converted Y co-ordinate
        public double Y { get; set; }
        public TimeSpan TimeStamp { get; set; }
    }

    public class Player : INotifyPropertyChanged
    {
        public Team Team { get; set; }
        private bool _visible;
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public LinkedList<Position> Positions { get; set; }
        public Position CurrentPosition { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }
        public PlayerShape Shape { get; set; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public static class PositionExtensions
    {
        public static double DistanceTo(this Position pos1, Position pos2)
        {
            return Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
        }


    }

    public class PlayerSpeed : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var player = ((ObservableCollection<Player>)values[0]).FirstOrDefault();
            var interval = (TimeSpan)values[1];

            if (player == null)
                return 0d;

            var distance = 0d;
            var previousPosition = player.Positions.Find(player.CurrentPosition);

            while(player.CurrentPosition.TimeStamp - previousPosition.Value.TimeStamp < interval)
            {
                distance += previousPosition.Value.DistanceTo(previousPosition.Previous.Value);
                previousPosition = previousPosition.Previous;
            }

            /*for (var i = 1; i < positions.Count; i++)
            {
                distance += positions[i].DistanceTo(positions[i - 1]);
            }*/

            var speed = distance / interval.TotalMilliseconds;

            //Convert speed from centimetres / millisecond to metres / second
            return Math.Round(speed * 10, 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var converter = new PlayerSpeed();
            var iterations = 10000;
            var results = new List<double>();
            var gameCreator = new GameCreator(new VisualDataConverter(), new CsvVisualReader());
            var ground = new Ground
            {
                Height = 1500,
                Width = 2800,
                Padding = 200,
                Name = "Wheel Chair Rugby",
                Type = GroundType.WheelChairRugby,
                CentreLatitude = 0,
                CentreLongitude = 0
            };
            Game game = null;

            using (var stream = new FileStream("E:\\Code\\RadiantTulip\\TestData\\Full Q1 with ball.txt", FileMode.Open))
            {
                game = gameCreator.CreateGame(stream, ground, null);
            }

            var player = game.Teams.First(t => t.Name == "Belgium").Players.First(p => p.Name == "Windey");
            player.CurrentPosition = player.Positions[player.Positions.Count / 2];
            var convertPlayer = ConvertPlayer(player);
            var parameters = new object[] { new ObservableCollection<Player> { convertPlayer }, TimeSpan.FromMilliseconds(1000) };

            var watch = new Stopwatch();

            for (var i = 0; i < 20; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                watch.Start();
                for (var j = 0; j < iterations; j++)
                {
                    converter.Convert(parameters, null, null, null);
                }
                watch.Stop();

                Console.WriteLine(watch.ElapsedTicks);
                watch.Reset();
            }
            Console.ReadLine();
        }

        private static Player ConvertPlayer(RadiantTulip.Model.Player player)
        {
            var result = new Player { Name = player.Name, Shape = player.Shape, Size = player.Size, Visible = true, Positions = new LinkedList<Position>() };
            foreach(var p in player.Positions.OrderBy(pos => pos.TimeStamp))
            {
                result.Positions.AddLast(new Position { TimeStamp = p.TimeStamp, X = p.X, Y = p.Y });
            }

            var middleTimeStamp = player.Positions[result.Positions.Count / 2].TimeStamp;
            result.CurrentPosition = result.Positions.First(p => p.TimeStamp == middleTimeStamp);
            return result;
        }
    }
}
