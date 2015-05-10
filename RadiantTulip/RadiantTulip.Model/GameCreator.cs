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

            if (reportProgress != null)
            {
                totalPositions = game.Teams.Sum(t => t.Players.Sum(p => p.Positions.Count()));
                progressIncrement = totalPositions / (MAX_PROGRESS - STARTING_PROGRESS);
                if (progressIncrement == 0)
                    progressIncrement = MIN_PROGRESS_INCREMENT;
                reportProgress(currentProgress);
            }

            foreach(var t in game.Teams)
                foreach (var p in t.Players)
                {
                    var positions = new List<Position>();
                    foreach (var pos in p.Positions)
                    {
                        countSinceLastUpdate++;
                        positions.Add(_converter.Convert(pos, ground));

                        if (reportProgress != null && countSinceLastUpdate == progressIncrement)
                        {
                            countSinceLastUpdate = 0;
                            currentProgress += (100 - STARTING_PROGRESS) / (totalPositions / progressIncrement);
                            reportProgress(currentProgress);
                        }
                    }
                    p.Positions = positions;
                }

            return game;
        }
    }
}
