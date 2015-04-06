using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests
{
    public static class TestFileHelper
    {
        public static Stream GetFilePath(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Concat("RadiantTulip.Tests.TestFiles.", fileName));
        }
    }
}
