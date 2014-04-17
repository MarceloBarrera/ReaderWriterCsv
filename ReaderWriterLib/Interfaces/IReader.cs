using System.Collections.Generic;


namespace ReaderWriterLib.Interfaces
{
    public interface IReader
    {
        List<KeyValuePair<string, List<string>>> ReadLines();
    }
}
