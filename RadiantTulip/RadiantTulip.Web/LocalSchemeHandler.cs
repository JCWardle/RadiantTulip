using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace RadiantTulip.Web
{
    public class LocalSchemeHandler : ISchemeHandler
    {
        private IFileSystem _fileSystem;

        private Dictionary<string, string> _mimeTypes = new Dictionary<string, string>()
        {
            { ".html", "text/html" },
            { ".css", "text/css" },
            { ".js", "text/javascript" }
        };

        public LocalSchemeHandler(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool ProcessRequestAsync(IRequest request, ISchemeHandlerResponse response, OnRequestCompletedHandler requestCompletedCallback)
        {
            var uri = new Uri(request.Url);
            string file = string.Empty;
            if (uri.AbsolutePath != "/")
                file = string.Format("{0}{1}", uri.Authority, uri.AbsolutePath);
            else
                file = uri.Authority;

            if (_fileSystem.Exists(file))
            {
                var fileContent = _fileSystem.ReadAllBytes(file);
                response.ResponseStream = new MemoryStream(fileContent);

                response.MimeType = _mimeTypes[Path.GetExtension(file)];
                requestCompletedCallback();
                return true;
            }
            return false;            
        }
    }
}
