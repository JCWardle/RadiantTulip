using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model.Input
{
    public class CsvVisualReader : ISpatialReader
    {
        private const int FPS = 25;

        public List<Team> GetTeams(System.IO.Stream stream)
        {
            var result = new List<Team>();
            var reader = new TextFieldParser(stream);
            reader.SetDelimiters(",");

            while(!reader.EndOfData)
            {
                var data = reader.ReadFields();

                var team = result.FirstOrDefault(t => t.Name == data[5]);

                if (team == null)
                {
                    team = new Team() { Name = data[5], Players = new List<Player>() };
                    result.Add(team);
                }

                var player = team.Players.FirstOrDefault(p => p.Name == data[0]);

                if(player == null)
                {
                    player = new Player { Name = data[0], Positions = new List<Position>() };
                    team.Players.Add(player);
                }

                var frame = double.Parse(data[1]);
                player.Positions.Add( new Position { X = double.Parse(data[2]), Y = double.Parse(data[3]), TimeStamp = TimeSpan.FromMilliseconds(100 / FPS * frame )  });
            }

            return result;
        }
    }
}