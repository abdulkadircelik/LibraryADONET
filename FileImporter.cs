using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LibraryADONET
{
    public class FileImporter<T> : IImporter
    {
        private string filePath;
        public List<T> List { get; set; }

        public FileImporter(string _filePath)
        {
            FilePath = _filePath;
        }
        public string FilePath { get => filePath; set => filePath = value; }
        
        public void Import()
        {
            List<T> records = new List<T>();
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader,CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                records = csv.GetRecords<T>().ToList();
            }
            List = records;
        }
    }
}
