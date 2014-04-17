using System.Globalization;
using System.IO;
using ReaderWriterLib.Interfaces;


namespace ReaderWriterLib
{
    public class WriterToFile : IWriter
    {
        private readonly string _pathAndFileName;
        private readonly char _delimiter;
        

        public WriterToFile(string pathAndFileName, char delimiter )
        {
            _pathAndFileName = pathAndFileName;
            _delimiter = delimiter;
        }

        public void WriteLine(string[] items)
        {
            var itemsTemp = new string[items.Length];
            int i = 0;
            foreach (var item in items)
            {
                itemsTemp[i] = item;
                i++;
            }
            var resultLine = string.Join(_delimiter.ToString(CultureInfo.InvariantCulture), itemsTemp);
            AppendLine(resultLine);
        }
       
        public void CreateFile()
        {
            var file =  new FileInfo(_pathAndFileName).Create();
            file.Close();
        }

        public bool FileExists()
        {
            var fileInfo = new FileInfo(_pathAndFileName);
            return fileInfo.Exists;
        }

        private void AppendLine(string line)
        {
            var fileInfo = new FileInfo(_pathAndFileName);
            using (var writerStream = fileInfo.AppendText())
            {
                writerStream.WriteLine(line);
            }
        }

    }
}
