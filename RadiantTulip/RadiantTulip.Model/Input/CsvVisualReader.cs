using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RadiantTulip.Model.Input
{
    public class CsvVisualReader : ISpatialReader
    {
        private const int FPS = 25;
        private const string BALL_NAME = "Ball";
        private List<Team> _teams = new List<Team>();
        private Ball _ball = new Ball() { Positions = new List<Position>(), Colour = Color.FromArgb(100, 255, 255, 0)};
        private int _startingFrame = -1;

        public List<Team> GetTeams(Stream stream)
        {
            var reader = new TextFieldParser(stream);
            var line = 0;
            reader.SetDelimiters(",");

            while(!reader.EndOfData)
            {
                line++;
                var data = reader.ReadFields();
                double x, y;
                int frame;

                if (data.Length < 6)
                    throw new ArgumentException(String.Format("Positional data on line {0} doesn't have enough columns, it requires 6 or more", line));

                if (!double.TryParse(data[2], out x) || !double.TryParse(data[3], out y) || !int.TryParse(data[1], out frame))
                    throw new ArgumentException(String.Format("A number in line {0} is not in a numerical format", line));

                var team = _teams.FirstOrDefault(t => t.Name == data[5]);

                if (team == null)
                {
                    team = CreateTeam(data[5]);
                    _teams.Add(team);
                }

                if (data[0] == BALL_NAME)
                    Ball(x, y, frame);
                else
                    Player(x, y, frame, data[0], team);
            }

            return _teams;
        }

        public Ball GetBall(Stream stream)
        {
            if (_startingFrame == -1)
                GetTeams(stream);

            return _ball;
        }

        private void Ball(double x, double y, int frame)
        {
            if(_startingFrame == -1)
                _startingFrame = frame;

            _ball.Positions.Add(new Position { X = x, Y = y, TimeStamp = TimeSpan.FromMilliseconds(1000 / FPS * (frame - _startingFrame)) });
        }

        private void Player(double x, double y, int frame, string name, Team team)
        {
            var player = team.Players.FirstOrDefault(p => p.Name == name);

            if (player == null)
            {
                player = CreatePlayer(name, _teams.IndexOf(team));
                team.Players.Add(player);
            }

            if (_startingFrame == -1)
                _startingFrame = frame;

            player.Positions.Add(new Position { X = x, Y = y, TimeStamp = TimeSpan.FromMilliseconds(1000 / FPS * (frame - _startingFrame)) });
        }

        private Team CreateTeam(string name)
        {
            return new Team() { Name = name, Players = new List<Player>() };
        }

        private Player CreatePlayer(string name, int teamNumber)
        {
            return new Player
            {
                Name = name,
                Positions = new List<Position>(),
                Visible = true,
                Size = Size.Medium,
                Shape = PlayerShape.Circle,
                Colour = teamNumber == 0 ? Color.FromRgb(0, 0, 0) : Color.FromRgb(255, 255, 255)
            };
        }
    }
}