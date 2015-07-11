using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RadiantTulip.API
{
    public interface IFileSystem
    {
        string[] GetFiles(string directory);
        Stream GetFileStream(string file);
    }
}
