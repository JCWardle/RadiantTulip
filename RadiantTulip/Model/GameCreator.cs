using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class GameCreator : IGameCreator
    {
        private ICoordinateConverter _converter;
        private ISpatialReader _reader;

        public GameCreator(ICoordinateConverter converter, ISpatialReader reader)
        {
            _converter = converter;
            _reader = reader;
        }

        public Game CreateGame(Stream stream)
        {
            var game = new Game();

            game.Teams = _reader.GetTeams(stream);

            foreach(var t in game.Teams)
                foreach (var p in t.Players)
                {
                    var positions = new List<Position>();
                    foreach (var pos in p.Positions)
                        positions.Add(_converter.Convert(pos));
                    p.Positions = positions;
                }

            return game;
        }
    }
}
