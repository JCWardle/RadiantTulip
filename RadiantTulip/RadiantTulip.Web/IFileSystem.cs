using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadiantTulip.Web
{
    public interface IFileSystem
    {
        bool Exists(string file);
        byte[] ReadAllBytes(string file);
    }
}
