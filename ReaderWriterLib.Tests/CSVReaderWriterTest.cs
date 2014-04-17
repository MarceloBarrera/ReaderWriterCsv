using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReaderWriterLib.Interfaces;

namespace ReaderWriterLib.Tests
{
    [TestClass]
    public class CSVReaderWriterTest
    {
        private const string PathAndFileNameOrigin = @"c:\TempWork\FileOrigin.txt";
        private const char DelimiterOrigin = '\t';
        
        [TestMethod]
        public void Write_DestinationCallsEqualsOriginFileLines()
        {
            var lines = GetLinesFromOriginFile();
            int linesOriginfile = lines.Count;

            var fakeWriter = new Mock<IWriter>();
            var fakeReader = new Mock<IReader>();
            string fileNameDest = @"c:\Destination.txt";
            char delimiterDest = ';';
            var csvReaderWriter = new CSVReaderWriter(fakeWriter.Object, fakeReader.Object, fileNameDest, delimiterDest);
            
            foreach (var item in lines)
                csvReaderWriter.Write(item.Value.ToArray());

            fakeWriter.Verify(a=>a.WriteLine(It.IsAny<string[]>()),Times.Exactly(linesOriginfile));

        }

        [TestMethod]
        public void Write_CreatesFileWhenfileDoesntExists()
        {   
            var fakeWriter = new Mock<IWriter>();

            bool firstTime = true;
            fakeWriter.Setup(a => a.FileExists()).Returns(() =>
                            {
                                if (!firstTime)
                                    return true;
                                firstTime = false;
                                return false;
                            }
                            );

            var fakeReader = new Mock<IReader>();
            string fileNameDest = @"c:\NotExistsDestination.txt";
            char delimiterDest = ';';
            var csvReaderWriter = new CSVReaderWriter(fakeWriter.Object, fakeReader.Object,fileNameDest,delimiterDest);
            
            var lines = GetLinesFromOriginFile();
            foreach (var item in lines)
                csvReaderWriter.Write(item.Value.ToArray());

            fakeWriter.Verify(a => a.CreateFile(),Times.AtMostOnce );

        }

        [TestMethod]
        public void ReadContents_FileHasLinesEqualsTrue()
        {
            var lines = GetLinesFromOriginFile();
            Assert.IsTrue(lines.Count > 0,"No contents on origin file: " + PathAndFileNameOrigin);
        }

        private List<KeyValuePair<string, List<string>>> GetLinesFromOriginFile()
        {
            var csvReader = new CSVReaderWriter(PathAndFileNameOrigin, DelimiterOrigin);
            csvReader.ReadContents();
            List<KeyValuePair<string, List<string>>> lines = csvReader.Columns;
            return lines;
        }


       
    }
}
