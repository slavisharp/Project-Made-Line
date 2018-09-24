namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IVlogManager
    {
        Task<Vlog> GetByIdAsync(int id);

        IQueryable<Vlog> GetQuery();

        IQueryable<Vlog> GetQueryById(int id);

        IQueryable<Vlog> GetQueryByAlias(string alias);

        ISearchResultModel<Vlog> SearchVlogs(ISearchVlogModel model);

        Task<IManagerActionResultModel<Vlog>> CreateVlogAsync(ICreateVlogModel model);

        Task<IManagerActionResultModel<Vlog>> UpdateVlogAsync(IUpdateVlogModel model);

        Task<IManagerActionResultModel<Vlog>> UpdateVlogTranslation(IUpdateTranslationVlogModel model);
    }
}
