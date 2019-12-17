using System.IO;

namespace DirectedGraphSearch.Services.Contracts
{
    public interface IFileHelperService
    {
        void DeleteFile(string path);
        bool FileExists(string path);
        string FileReadAllText(string path);
        void WriteAllText(string path, string content);
        bool DirectoryExists(string path);
        DirectoryInfo CreateDirectory(string path);
        string[] GetFilesFromDirectory(string path, string filter, SearchOption searchOption);
    }
}
