using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RadiantTulip.View
{
    public class GlobalExceptionHandler : IGlobalExceptionHandler
    {
        private ILog _logger;

        public GlobalExceptionHandler(ILog logger)
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = logger;
        }

        public void HandleException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Fatal(e.Exception);
        }
    }
}
