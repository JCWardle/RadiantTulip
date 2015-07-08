using RadiantTulip.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RadiantTulip.Web
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var apiRunner = new APIRunner();
            var apiThread = new Thread(new ThreadStart(apiRunner.Run));
            apiThread.Start();
        }
    }
}
