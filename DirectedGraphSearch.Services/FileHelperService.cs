using System.IO;
using DirectedGraphSearch.Services.Contracts;

namespace DirectedGraphSearch.Services
{
    public class FileHelperService : IFileHelperService
    {
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string FileReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public string[] GetFilesFromDirectory(string path, string filter, SearchOption searchOption)
        {
            return Directory.GetFiles(path, filter, searchOption);
        }
    }
}
