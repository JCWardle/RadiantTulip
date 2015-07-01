using System.IO;

namespace RadiantTulip.Web
{
    public class FileSystem : IFileSystem
    {
        public bool Exists(string file)
        {
            return File.Exists(file);
        }

        public byte[] ReadAllBytes(string file)
        {
            return File.ReadAllBytes(file);
        }
    }
}
