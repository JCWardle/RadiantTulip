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

        public ModelUpdater(Game game)
        {
            _game = game;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public Game Game
        {
            get { return _game; }
        }


        public DateTime Time
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
