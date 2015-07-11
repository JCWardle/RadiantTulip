using Moq;
using NUnit.Framework;
using RadiantTulip.API;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Tests.Api
{
    [TestFixture]
    public class FieldControllerTests
    {
        private Stream _stream = new MemoryStream(Encoding.UTF8.GetBytes("123"));
        [Test]
        public void Field_Controller_Gets_Grounds_From_File()
        {
            var reader = new Mock<IGroundReader>();
            reader.Setup(r => r.ReadGrounds(It.IsAny<IList<Stream>>()))
                .Returns(new List<Ground>() 
                {
                    new Ground 
                    {
                        Width = 200,
                        Height = 200
                    }
                });
            var fileSystem = new Mock<IFileSystem>();
            fileSystem.Setup(f => f.GetFiles(It.IsAny<string>()))
                .Returns(new string[] { "file" });
            fileSystem.Setup(f => f.GetFileStream("file"))
                .Returns(_stream);

            var controller = new FieldController(reader.Object, fileSystem.Object);

            var result = controller.Get();

            Assert.AreEqual(1, result.Count());
            var ground = result.First();
            Assert.AreEqual(200, ground.Width);
            Assert.AreEqual(200, ground.Height);

            fileSystem.Verify(f => f.GetFileStream("file"), Times.Once);
            reader.Verify(r => r.ReadGrounds(It.IsAny<IList<Stream>>()), Times.Once);
        }
    }
}
