namespace MadeLine.Data.Repository
{
    using MadeLine.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class RepositoryExtensions
    {
        public static IQueryable<T> GetRange<T, K>(this IRepository<T> repository, IEnumerable<K> ids)
            where T: class, IKeyEntity<K>
        {
            IQueryable<T> entities = null;
            if (ids != null && ids.Any())
            {
                entities = repository.All().Where(e => ids.Contains(e.Id));
            }

            return entities;
        }
    }
}
