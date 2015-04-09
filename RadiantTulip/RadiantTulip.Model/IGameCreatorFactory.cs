using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public interface IGameCreatorFactory
    {
        IGameCreator CreateGameCreator(string filePath);
    }
}
