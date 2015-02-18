using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.View.ViewModels
{
    public class GameSetup : IGameSetup
    {
        private OberservableGround _ground;
        private string _positionalData;

        public OberservableGround Ground
        {
            get
            {
                return _ground;
            }
            set
            {
                _ground = value;
            }
        }

        public string PositionalData
        {
            get
            {
                return _positionalData;
            }
            set
            {
                _positionalData = value;
            }
        }
    }
}
