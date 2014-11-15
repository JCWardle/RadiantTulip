using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Player CreatePlayer(IXLWorksheet sheet)
        {
            var result = new Player
            {
                Visible = true,
                Positions = new List<Position>()
            };
            var timeStamp = new DateTime(1, 1, 1, 0, 0, 0, 0);

            var row = sheet.FirstRow();
            while(!row.Cell(1).IsEmpty())
            {
                if(!row.Cell(3).IsEmpty() && !row.Cell(4).IsEmpty())
                {
                    result.Positions.Add(new Position
                        {
                            TimeStamp = timeStamp,
                            X = row.Cell(3).GetDouble(),
                            Y = row.Cell(4).GetDouble()
                        });
                    timeStamp = timeStamp.Add(new TimeSpan(0, 0, 0, 0, 10));
                }
                row = row.RowBelow();
            }

            return result;
        }
    }
}
