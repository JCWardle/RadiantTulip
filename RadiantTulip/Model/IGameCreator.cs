using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public interface IGameCreator
    {
        Game CreateGame(Stream stream);
    }
}
