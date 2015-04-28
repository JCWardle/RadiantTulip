using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RadiantTulip.Model.Input
{
    public class CsvVisualReader : ISpatialReader
    {
        private const int FPS = 25;

        public List<Team> GetTeams(System.IO.Stream stream)
        {
            var result = new List<Team>();
            var reader = new TextFieldParser(stream);
            var line = 0;
            var startingFrame = -1;
            reader.SetDelimiters(",");

            while(!reader.EndOfData)
            {
                line++;
                var data = reader.ReadFields();

                if (data.Length < 6)
                    throw new ArgumentException(String.Format("Positional data on line {0} doesn't have enough columns, it requires 6 or more", line));

                var team = result.FirstOrDefault(t => t.Name == data[5]);

                if (team == null)
                {
                    team = new Team() { Name = data[5], Players = new List<Player>() };
                    result.Add(team);
                }

                var player = team.Players.FirstOrDefault(p => p.Name == data[0]);

                if(player == null)
                {
                    player = new Player 
                    { 
                        Name = data[0], 
                        Positions = new List<Position>(), 
                        Visible = true, 
                        Size = Size.Medium, 
                        Shape = PlayerShape.Circle,
                        Colour = result.IndexOf(team) == 0 ? Color.FromRgb(0, 0, 0) : Color.FromRgb(255, 255, 255)
                    };
                    team.Players.Add(player);
                }

                double x, y;
                int frame;

                if (!double.TryParse(data[2], out x) || !double.TryParse(data[3], out y) || !int.TryParse(data[1], out frame))
                    throw new ArgumentException(String.Format("A number in line {0} is not in a numerical format", line));

                if (startingFrame == -1)
                    startingFrame = frame;

                player.Positions.Add(new Position { X = x, Y = y, TimeStamp = TimeSpan.FromMilliseconds(100 / FPS * (frame - startingFrame)) });
            }

            return result;
        }
    }
}