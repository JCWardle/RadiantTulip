using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Model.Mocks
{
    internal class MockProgressReporter
    {
        public int Calls { get; set; }
        public int Progress {get; set; }

        public void ReportProgress(int progress)
        {
            Calls++;
            Progress = progress;
        }
    }
}
