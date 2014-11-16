using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class ModelUpdater : IModelUpdater
    {
        private Game _game;
        private DateTime _time;
        private TimeSpan _increment;
        private bool _incrementTime;

        public ModelUpdater(Game game)
        {
            if (game.Teams == null || !game.Teams.Any())
                throw new ArgumentException("You need to have a team");
            if (!game.Teams.Any(t => t.Players != null && t.Players.Any()))
                throw new ArgumentException("You must have atleast one player");

            _game = game;
            _time = game.Teams.First().Players.First().Positions.Min(p => p.TimeStamp);

            _increment = game.Teams.First().Players.First().Positions.Where(p => p.TimeStamp != _time).Min(p => p.TimeStamp) - _time;

            _incrementTime = false;
            Update();
        }

        public void Update()
        {
            if (!_incrementTime)
                _incrementTime = true;
            else
                _time += _increment;

            foreach (var t in _game.Teams)
                foreach (var player in t.Players)
                    player.CurrentPosition = player.Positions.First(p => p.TimeStamp == _time);
        }

        public Game Game
        {
            get { return _game; }
        }


        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                var minTime = _game.Teams.First().Players.First().Positions.Min(p => p.TimeStamp);
                var maxTime = _game.Teams.First().Players.First().Positions.Max(p => p.TimeStamp);

                if (value >= minTime && value <= maxTime)
                {
                    _time = value;
                    _incrementTime = false;
                }                    
                else
                    throw new ArgumentException("Time out of range");
            }
        }
    }
}
