namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProductManager
    {
        Task<Product> GetByIdAsync(int id);
        
        IQueryable<Product> GetQuery();

        IQueryable<Product> GetQueryById(int id);

        IQueryable<Product> SearchProduct(ISearchProductModel model);

        Task<IManagerActionResultModel<Product>> CreateProductAsync(ICreateProductModel model);
    }
}
