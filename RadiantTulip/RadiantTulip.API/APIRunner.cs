using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RadiantTulip.API
{
    public class APIRunner : IApiRunner
    {
        public void Run()
        {
            using(WebApp.Start<StartUp>(url: "http://localhost:9001"))
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
