using System;
using System.Linq;
using System.Collections.Generic;
using ReaderWriterLib.Interfaces;

namespace ReaderWriterLib
{
    /// <summary>
    /// Class for reading/writing to a text file 
    /// Default delimiter is tab '\t'
    /// </summary>
    public class CSVReaderWriter
    {
        private readonly IWriter _writer;
        private readonly IReader _reader;
        /// <summary>
        /// List of columns with their contents as a KeyValuePair.
        /// The key for each column list is the column name
        /// The value is the list of strings which make up the column
        /// </summary>
        public List<KeyValuePair<string, List<string>>> Columns { get; private set; }

        /// <summary>
        /// File being read/written to
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Column delimiter as a char eg. '\t'
        /// </summary>
        public char Delimiter { get; set; }

     
        /// <summary>
        /// Constructor: Set Filename
        /// Initializes the Columns member and defaults delimiter to tab '\t'
        /// </summary>
        /// <param name="fileName"></param> 
        public CSVReaderWriter(string fileName)
        {
            Columns = new List<KeyValuePair<string, List<string>>>();
            FileName = fileName;
            Delimiter = '\t';
            
            if (_writer == null)
                _writer = new WriterToFile(FileName, Delimiter);
            if (_reader == null)
                _reader = new ReaderFromFile(FileName, Delimiter);
        }

        public CSVReaderWriter(IWriter writer, IReader reader,string fileName,char delimiter)
        {
            _writer = writer;
            _reader = reader;
            FileName = fileName;
            Delimiter = delimiter;
        }
        /// <summary>
        /// Constructor: Set Filename and Delimiter
        /// </summary>
        /// <param name="fileName"></param>  
        /// <param name="delimiter"></param>
        public CSVReaderWriter(string fileName, char delimiter) : this(fileName)
        {
            Delimiter = delimiter;
            _writer = new WriterToFile(FileName, Delimiter); 
            _reader = new ReaderFromFile(FileName,Delimiter);
        }

        /// <summary>
        /// If file Exists appends text to end of the file otherwise creates new file and appends the text to it.
        /// </summary>
        /// <param name="columns"></param>
        public void Write(params string[] columns)
        {
            if (!_writer.FileExists())
                _writer.CreateFile();
           _writer.WriteLine(columns);
        }

        /// <summary>
        /// Write columns to file, appending text to end of the file
        /// </summary>
        /// <param name="appendToEndOfFile"></param>
        /// <param name="columns"></param>
        [Obsolete("This method is deprecated. use Write() instead")]
        public void Write(bool appendToEndOfFile, params string[] columns)
        {
            Write(columns);
        }

        
        ///<summary>
        /// Read contents of file and return boolean indicating whether there's any content or not
        /// </summary>
        /// <returns></returns>
        public bool ReadContents()
        {
            Columns = _reader.ReadLines();
            return Columns.Any();
        }

        /// <summary>
        /// This enum is left here for backwards compatibility
        /// </summary>
        [Obsolete("No longer required.")]
        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        /// <summary>
        /// Parameterless constructor for backwards compatibility 
        /// Filename is blank
        /// </summary>
        [Obsolete("Use constructor with fileName parameter - CSVReaderWriter(string fileName)")]
        public CSVReaderWriter()
            : this("")
        {
        }

        /// <summary>
        /// This method has only been left for backwards compatibility
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="mode"></param>
        [Obsolete("This method is deprecated. Instantiate class with filename and optional delimiter instead.")]
        public void Open(string fileName, Mode mode)
        {
            FileName = fileName;

            switch (mode)
            {
                case Mode.Read:
                    ReadContents();
                    break;
                case Mode.Write:
                    break;
                default:
                    throw new Exception("Unknown file mode for " + fileName);
            }
        }

        
        /// <summary>
        /// This method has only been left for backwards compatibility
        /// </summary>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <returns></returns>
        [Obsolete("This method is deprecated, please use ReadContents() instead.")]
        public bool Read(string column1, string column2)
        {
            return Read(out column1, out column2);
        }


        /// <summary>
        /// This method has only been left for backwards compatibility
        /// </summary>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <returns></returns>
        [Obsolete("This method is deprecated, please use ReadContents() instead.")]
        public bool Read(out string column1, out string column2)
        {
            //Set [Columns] variable
            var read = ReadContents();
            column1 = Columns[0].Key; //First Column
            column2 = Columns[1].Key; //Second Column
            return read;
        }
        

        /// <summary>
        /// This method has only been left for backwards compatibility.
        /// _readerStream and _writerStream use 'using' statement which provides a convenient syntax 
        /// that ensures the correct use of IDisposable objects.
        /// </summary>
        [Obsolete("This method is deprecated. Using 'using' statement")]
        public void Close()
        {
        }
    }
}
