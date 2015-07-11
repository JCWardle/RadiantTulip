using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RadiantTulip.API
{
    public class FieldController : ApiController
    {
        private const string FILE_DIRECTORY = "Grounds\\";
        private IList<Ground> _grounds;

        public FieldController(IGroundReader reader, IFileSystem fileSystem)
        {
            var files = fileSystem.GetFiles(FILE_DIRECTORY);

            var streams = files.Select(f => fileSystem.GetFileStream(f)).ToList();

            _grounds = reader.ReadGrounds(streams);
        }

        /// <summary>
        /// Retrieves a set of all the ground types that are supported
        /// </summary>
        /// <returns>The grounds</returns>
        public IEnumerable<Ground> Get()
        {
            return _grounds;
        }
    }
}
