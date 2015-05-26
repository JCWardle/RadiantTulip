using System;
using System.Collections.Generic;
using System.Linq;

namespace RadiantTulip.Model
{
    public class ModelUpdater : IModelUpdater
    {
        private enum State { Backwards, Forwards };
        private Game _game;
        private TimeSpan _time;
        private TimeSpan _increment;
        private TimeSpan _max;
        private bool _incrementTime;
        private State _state;

        public ModelUpdater(Game game)
        {
            if (game.Teams == null || !game.Teams.Any())
                throw new ArgumentException("You need to have a team");
            if (!game.Teams.Any(t => t.Players != null && t.Players.Any()))
                throw new ArgumentException("You must have atleast one player");

            _game = game;
            _time = game.Teams.First().Players.First().Positions.Min(p => p.TimeStamp);
            _max = game.Teams.First().Players.First().Positions.Max(p => p.TimeStamp);

            _increment = game.Teams.First().Players.First().Positions.Where(p => p.TimeStamp != _time).Min(p => p.TimeStamp) - _time;
            

            _incrementTime = false;
            _state = State.Forwards;
            Update();
        }

        /// <summary>
        /// Updates the game by changing all the current positions.
        /// This method exploits the ordered linked list to quickly select the next position in the list
        /// 
        /// In the case where the time is set by the user, the variable _incrementTime is set
        /// to false. When this is the case we must look up all of the positions in the linked list which is a lengthy procedure
        /// </summary>
        public void Update()
        {
            if (_time == _max && _increment > TimeSpan.Zero || _time == TimeSpan.Zero && _increment < TimeSpan.Zero)
                return;

            var lookupTime = !_incrementTime;
            if (!_incrementTime)
                _incrementTime = true;
            else
                _time += _increment;

            if (_game.Ball != null && _game.Ball.Positions != null)
            {
                _game.Ball.CurrentPosition = MovePosition(_game.Ball.CurrentPosition, _game.Ball.Positions, lookupTime);
            }

            foreach (var t in _game.Teams)
            {
                foreach (var player in t.Players)
                {
                    player.CurrentPosition = MovePosition(player.CurrentPosition, player.Positions, lookupTime);
                }
            }
        }

        private LinkedListNode<Position> MovePosition(LinkedListNode<Position> currentPosition, LinkedList<Position> positions, bool lookup)
        {
            if (currentPosition == null || lookup)
            {
                var newPosition = positions.FirstOrDefault(p => p.TimeStamp == _time);
                return positions.Find(newPosition);
            }
            else if (_state == State.Forwards)
            {
                var newPosition = currentPosition.Next;
                if (newPosition == null || newPosition.Value.TimeStamp != _time)
                    return null;

                return newPosition;                
            }
            else if (_state == State.Backwards)
            {
                var newPosition = currentPosition.Previous;
                if (newPosition == null || newPosition.Value.TimeStamp != _time)
                    return null;

                return newPosition;
            }
            return null;
        }

        public void ChangeDirection()
        {
            _increment = -_increment;
            if (_increment < TimeSpan.Zero)
                _state = State.Backwards;
            else
                _state = State.Forwards;
        }

        public Game Game
        {
            get { return _game; }
        }


        public TimeSpan Time
        {
            get
            {
                return _time;
            }
            set
            {
                var minTime = _game.Teams.First().Players.First().Positions.Min(p => p.TimeStamp);

                if (value >= minTime && value <= _max)
                {
                    _time = value;
                    _incrementTime = false;
                }                    
                else
                    throw new ArgumentException("Time out of range");
            }
        }

        public TimeSpan Increment
        {
            get
            {
                return _increment;
            }
        }

        public TimeSpan MaxTime
        {
            get
            {
                return _max;
            }
        }
    }
}
