using System.IO;

namespace Domain.Util
{
    public class FileBytesAbstraction : TagLib.File.IFileAbstraction
    {
        public FileBytesAbstraction(string name, byte[] bytes)
        {
            Name = name;
            MemoryStream ms = new MemoryStream(bytes);
            ReadStream = ms;
            WriteStream = ms;
        }   

        public void CloseStream(Stream stream)
        {
            stream.Dispose();
        }

        public string Name { get; private set; }
        public Stream ReadStream { get; private set;}
        public Stream WriteStream { get; private set;}
    }
}