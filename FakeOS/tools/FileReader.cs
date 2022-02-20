using System.IO;

namespace FakeOS
{
    public static class FileReader
    {
        
        public static string getFileString(string path)
        {
            return File.ReadAllText(path);
        }
        
        public static string[] getFileLines(string path)
        {
            return File.ReadAllLines(path);
        }
        
        
    }
}