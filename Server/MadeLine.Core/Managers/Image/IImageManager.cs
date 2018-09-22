namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IImageManager
    {
        Task<Image> SaveImageAsync(IFormFile image);

        Task<Image> GetByIdAsync(int id);

        IQueryable<Image> GetQuery();

        IQueryable<Image> GetQueryById(int id);

        IQueryable<Image> GetQueryByGuid(string guid);
    }
}
