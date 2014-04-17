using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReaderWriterLib.Interfaces;

namespace ReaderWriterLib
{
    public class ReaderFromFile:IReader
    {
        private readonly string _pathAndFileName;
        private readonly char _delimiter;
        public ReaderFromFile(string pathAndFileName, char delimiter)
        {
            _pathAndFileName = pathAndFileName;
            _delimiter = delimiter;
        }
        public List<KeyValuePair<string, List<string>>> ReadLines()
        {
            var res = new List<KeyValuePair<string, List<string>>>();
            using (var readerStream = File.OpenText(_pathAndFileName))
            {
                var lineNumber = 0;
                while (!readerStream.EndOfStream)
                {
                    var line = readerStream.ReadLine();
                    if (line != null)
                    {
                        var columns = line.Split(_delimiter);
                        var keyValuePair = new KeyValuePair<string, List<string>>(
                                                        string.Format("Column{0}", lineNumber),
                                                        columns.ToList());
                        res.Add(keyValuePair);
                        lineNumber++;
                    }
                }
            }
            return res;
        }
    }
}
