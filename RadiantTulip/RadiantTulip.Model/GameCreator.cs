using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace RadiantTulip.Model
{
    public class GameCreator : IGameCreator
    {
        private ICoordinateConverter _converter;
        private ISpatialReader _reader;
        private const int STARTING_PROGRESS = 50;
        private const int MAX_PROGRESS = 100;
        private const int MIN_PROGRESS_INCREMENT = 10;

        public GameCreator(ICoordinateConverter converter, ISpatialReader reader)
        {
            _converter = converter;
            _reader = reader;
        }

        public Game CreateGame(Stream spatialData, Ground ground, Action<int> reportProgress)
        {
            var game = new Game() { Ground = ground };

            game.Teams = _reader.GetTeams(spatialData);
            game.Ball = _reader.GetBall(spatialData);

            var currentProgress = STARTING_PROGRESS;
            var progressIncrement = 0;
            var countSinceLastUpdate = 0;
            var totalPositions = 0;

            IEnumerable<Position> positions = new List<Position>();
            foreach (var t in game.Teams)
            {
                t.Players.ForEach(p => positions = positions.Concat(p.Positions));
            }

            if(game.Ball != null && game.Ball.Positions != null)
                positions = positions.Concat(game.Ball.Positions);

            if (reportProgress != null)
            {
                totalPositions = positions.Count();
                progressIncrement = totalPositions / (MAX_PROGRESS - STARTING_PROGRESS);
                if (progressIncrement == 0)
                    progressIncrement = MIN_PROGRESS_INCREMENT;
                reportProgress(currentProgress);
            }

            foreach(var p in positions)
            {
                countSinceLastUpdate++;
                var newPosition = _converter.Convert(p, ground);
                p.X = newPosition.X;
                p.Y = newPosition.Y;
                p.TimeStamp = newPosition.TimeStamp;

                if (reportProgress != null && countSinceLastUpdate == progressIncrement)
                {
                    countSinceLastUpdate = 0;
                    currentProgress += (100 - STARTING_PROGRESS) / (totalPositions / progressIncrement);
                    reportProgress(currentProgress);
                }                
            }

            return game;
        }
    }
}
