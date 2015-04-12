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
        public void HandleException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
