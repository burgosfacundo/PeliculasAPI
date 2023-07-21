namespace PeliculasAPI.Services
{
    public class LocalFileStore : IStoreFile
    {
        private readonly IWebHostEnvironment environment;
        private readonly IHttpContextAccessor accessor;

        public LocalFileStore(IWebHostEnvironment environment, IHttpContextAccessor accessor)
        {
            this.environment = environment;
            this.accessor = accessor;
        }

        public Task DeleteFilte(string container, string route)
        {
            if(route != null)
            {
                var nameFile = Path.GetFileName(route);
                string directory = Path.Combine(environment.WebRootPath, container, nameFile);

                if(File.Exists(directory))
                {
                    File.Delete(directory);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            await DeleteFilte(container, route);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            var name = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(environment.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, name);
            await   File.WriteAllBytesAsync(ruta, content);

            var actualUrl = $"{accessor.HttpContext.Request.Scheme}://{accessor.HttpContext.Request.Host}";
            var urlDB = Path.Combine(actualUrl,container,name).Replace('\\', '/');

            return urlDB;
        }
    }
}
