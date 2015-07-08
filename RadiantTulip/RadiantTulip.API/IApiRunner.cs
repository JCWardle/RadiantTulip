using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.API
{
    public interface IApiRunner
    {
        void Run();
        void Stop();
    }
}
