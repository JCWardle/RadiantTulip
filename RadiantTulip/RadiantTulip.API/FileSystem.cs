using System.Collections.Generic;
using System.IO;

namespace RadiantTulip.API
{
    public class FileSystem : IFileSystem
    {

        public string[] GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public Stream GetFileStream(string file)
        {
            return File.OpenRead(file);
        }
    }
}
