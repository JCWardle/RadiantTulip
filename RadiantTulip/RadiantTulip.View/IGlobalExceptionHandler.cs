using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RadiantTulip.View
{
    public interface IGlobalExceptionHandler
    {
        void HandleException(object sender, DispatcherUnhandledExceptionEventArgs e);
    }
}
