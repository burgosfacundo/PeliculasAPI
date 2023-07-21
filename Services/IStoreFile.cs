namespace PeliculasAPI.Services
{
    public interface IStoreFile
    {
        Task<string> EditFile(byte[] content,string extension, string container, string route,string contentType);
        Task DeleteFilte(string container, string route);
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}
