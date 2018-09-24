namespace MadeLine.Core.Managers
{
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class ImageManager : IImageManager
    {
        private AppSettings settings;
        private IHostingEnvironment environment;
        private IRepository<Image> repository;

        public ImageManager(IOptions<AppSettings> options, IHostingEnvironment env, IRepository<Image> repo)
        {
            this.settings = options.Value;
            this.environment = env;
            this.repository = repo;
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await this.repository.GetByIdAsync(id);
        }

        public IQueryable<Image> GetQuery()
        {
            return this.repository.All();
        }

        public IQueryable<Image> GetQueryByGuid(string guid)
        {
            return this.repository.All().Where(i => i.GuidName == guid);
        }

        public IQueryable<Image> GetQueryById(int id)
        {
            return this.repository.All().Where(i => i.Id == id);
        }

        public async Task<Image> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("No image is passed!");
            }

            string fileName = image.FileName;
            string fileExtension = fileName.SubstringFromLast(".").ToLowerInvariant();
            if (!StaticVariables.IMAGE_EXTENSIONS.Contains(fileExtension))
            {
                throw new ArgumentException("Unsupported image format!");
            }
            
            string fullPath = Path.Combine(environment.WebRootPath, settings.ImagesRelativePath.Replace("/", string.Empty));
            string guidName = $"{Guid.NewGuid().ToString()}.{fileExtension}";
            string fullFileName = Path.Combine(fullPath, guidName);
            string relativeUrl = $"{settings.ImagesRelativePath}/{guidName}";
            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(fullFileName, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            var entity = new Image()
            {
                Extension = fileExtension,
                GuidName = guidName,
                MimeType = image.ContentType,
                Name = fileName,
                URL = relativeUrl
            };

            this.repository.Add(entity);
            await this.repository.SaveAsync();
            return entity;
        }
    }
}
