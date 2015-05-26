using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace RadiantTulip.Model.Input
{
    public class ExcelReader : ISpatialReader
    {
        public List<Team> GetTeams(Stream stream)
        {
            var book = new XLWorkbook(stream);
            var team = new Team
                {
                    Players = new List<Player>()
                };

            foreach(var sheet in book.Worksheets)
            {
                var player = CreatePlayer(sheet);
                player.Team = team;
                team.Players.Add(player);    
            }

            return new List<Team>() { team };
        }
        
        public Ball GetBall(Stream stream)
        {
            //TODO: Implement when data arrives
            return null;
        }

        private Player CreatePlayer(IXLWorksheet sheet)
        {
            var result = new Player
            {
                Visible = true,
                Positions = new LinkedList<Position>(),
                Name = sheet.Name,
                Size = Size.Medium,
                Colour = Color.FromRgb(255, 0, 0)
            };

            var time = new TimeSpan();

            var row = sheet.FirstRow();
            while(!row.Cell(1).IsEmpty())
            {
                if(!row.Cell(3).IsEmpty() && !row.Cell(4).IsEmpty())
                {
                    result.Positions.AddLast(new Position
                        {
                            TimeStamp = time,
                            Y = row.Cell(3).GetDouble(),
                            X = row.Cell(4).GetDouble()
                        });
                    time = time.Add(new TimeSpan(0,0,0,0, 100));
                }
                row = row.RowBelow();
            }

            return result;
        }
    }
}
