namespace ReaderWriterLib.Interfaces
{
    public interface IWriter
    {
        void WriteLine(string[] items);

        void CreateFile();

        bool FileExists();
    }
}
