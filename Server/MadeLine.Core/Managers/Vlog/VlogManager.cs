namespace MadeLine.Core.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.Extensions.Options;

    public class VlogManager : IVlogManager
    {
        private IRepository<Vlog> repository;
        private AppSettings settings;

        public VlogManager(IRepository<Vlog> repo, IOptions<AppSettings> options)
        {
            this.repository = repo;
            this.settings = options.Value;
        }

        public async Task<IManagerActionResultModel<Vlog>> CreateVlogAsync(ICreateVlogModel model)
        {
            var result = new ManagerActionResultModel<Vlog>();
            try
            {
                string alias = model.Alias.ToLowerInvariant();
                bool isExisting = this.repository.All().Where(v => v.Alias == alias).Any();
                if (isExisting)
                {
                    result.Errors.Add(new ErrorResultModel()
                    {
                        Code = nameof(model.Name),
                        Message = $"Vlog with alias {model.Alias} already exists!"
                    });
                    return result;
                }

                var entity = new Vlog()
                {
                    Alias = alias,
                    Name = model.Name,
                    EmbededVideo = model.EmbededVideo,
                    ThumbnailId = model.ThumbImageId
                };

                entity.Translations.Add(new VlogTranslation()
                {
                    Vlog = entity,
                    Language = this.settings.DefaultLanguage,
                    Name = model.Name
                });

                await this.repository.AddAsync(entity);
                await this.repository.SaveAsync();
                result.Succeeded = true;
                result.Model = entity;
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ErrorResultModel(ex));
                if (ex.InnerException != null)
                {
                    result.Errors.Add(new ErrorResultModel(ex.InnerException));
                }
            }

            return result;
        }

        public async Task<Vlog> GetByIdAsync(int id)
        {
            return await this.repository.GetByIdAsync(id);
        }

        public IQueryable<Vlog> GetQuery()
        {
            return this.repository.All();
        }

        public IQueryable<Vlog> GetQueryByAlias(string alias)
        {
            return this.repository.All().Where(v => v.Alias == alias.ToLowerInvariant());
        }

        public IQueryable<Vlog> GetQueryById(int id)
        {
            return this.repository.All().Where(v => v.Id == id);
        }

        public ISearchResultModel<Vlog> SearchVlogs(ISearchVlogModel model)
        {
            var query = this.repository.All();
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Name))
                {
                    if (model.Language != null)
                    {
                        query = query.Where(v => v.Translations.Where(t => t.Language == model.Language && t.Name.Contains(model.Name)).Any());
                    }
                    else
                    {
                        query = query.Where(v => v.Name.Contains(model.Name));
                    }
                }
            }

            int page = model != null ? model.Page : 1;
            int pageSize = model != null ? model.PageSize : StaticVariables.DEFAULT_PAGE_SIZE;
            int totalCount = query.Count();
            int pageCount = totalCount / pageSize;
            if (totalCount % pageSize > 0)
            {
                pageCount++;
            }

            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1 || pageSize > StaticVariables.MAX_PAGE_SIZE)
            {
                pageSize = StaticVariables.DEFAULT_PAGE_SIZE;
            }

            query = query.OrderBy(m => m.Name).Skip((page - 1) * pageSize).Take(pageSize);
            return new SearchResultModel<Vlog>()
            {
                List = query,
                Page = page,
                PageSize = pageSize,
                PageCount = pageCount,
                TotalCount = totalCount
            };
        }

        public async Task<IManagerActionResultModel<Vlog>> UpdateVlogAsync(IUpdateVlogModel model)
        {
            var result = new ManagerActionResultModel<Vlog>();
            try
            {
                Vlog entity = await this.GetByIdAsync(model.Id);
                if (entity == null)
                {
                    result.Succeeded = false;
                    result.Errors.Add(new ErrorResultModel(string.Empty, "Vlog not found!"));
                }

                VlogTranslation translation = entity.Translations.Where(t => t.Language == this.settings.DefaultLanguage).FirstOrDefault();
                entity.Name = model.Name;
                entity.Alias = model.Alias;
                entity.EmbededVideo = model.EmbededVideo;
                entity.ThumbnailId = model.ThumbImageId;
                translation.Name = model.Name;
                await this.repository.SaveAsync();
                result.Succeeded = true;
                result.Model = entity;
            }
            catch (Exception ex)
            {
                result.Errors.Add(new ErrorResultModel(ex));
                if (ex.InnerException != null)
                {
                    result.Errors.Add(new ErrorResultModel(ex.InnerException));
                }
            }

            return result;
        }

        public async  Task<IManagerActionResultModel<Vlog>> UpdateVlogTranslation(IUpdateTranslationVlogModel model)
        {
            var result = new ManagerActionResultModel<Vlog>();
            Vlog entity = await this.GetByIdAsync(model.Id);
            if (entity == null)
            {
                result.Succeeded = false;
                result.Errors.Add(new ErrorResultModel(string.Empty, "Vlog not found!"));
            }

            if (model.Language == this.settings.DefaultLanguage)
            {
                model.Name = model.Name;
            }

            VlogTranslation translation = entity.Translations.Where(t => t.Language == model.Language).FirstOrDefault();
            if (translation == null)
            {
                translation = new VlogTranslation()
                {
                    Vlog = entity,
                    VlogId = entity.Id,
                    Language = model.Language
                };
                entity.Translations.Add(translation);
            }

            translation.Name = model.Name;
            await this.repository.SaveAsync();

            return result;
        }
    }
}
