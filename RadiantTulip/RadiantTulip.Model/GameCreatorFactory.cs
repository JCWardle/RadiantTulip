using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiantTulip.Model
{
    public class GameCreatorFactory : IGameCreatorFactory
    {
        public IGameCreator CreateGameCreator(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            if(extension == ".txt")
            {
                return new GameCreator(new VisualDataConverter(), new CsvVisualReader());
            }
            else if (extension == ".xls" || extension == ".xlsx")
            {
                return new GameCreator(new GPSConverter(), new ExcelReader());
            }

            throw new NotImplementedException("File type not supported");
        }
    }
}
