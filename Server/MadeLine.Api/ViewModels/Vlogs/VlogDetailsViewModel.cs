namespace MadeLine.Api.ViewModels.Vlogs
{
    using MadeLine.Api.ViewModels.Images;
    using MadeLine.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class VlogDetailsViewModel
    {
        public int Id { get; set; }

        public string Name
        {
            get
            {
                if (this.Translation != null)
                {
                    return this.Translation.Name;
                }
                else
                {
                    return this.DefaultTranslation.Name;
                }
            }
        }

        public string Alias { get; set; }

        public string EmbededVideo { get; set; }

        public ImageDetailsViewModel Thumbnail { get; set; }

        private VlogTranslationDetails DefaultTranslation { get; set; }

        private VlogTranslationDetails Translation { get; set; }

        public static Expression<Func<Vlog, VlogDetailsViewModel>> FromEntity(TranslationLanguage language)
        {
            return x => new VlogDetailsViewModel()
            {
                Id = x.Id,
                Alias = x.Alias,
                EmbededVideo = x.EmbededVideo,
                Thumbnail = new ImageDetailsViewModel()
                {
                    Id = x.Thumbnail.Id,
                    Name = x.Thumbnail.Name,
                    Url = x.Thumbnail.URL
                },
                DefaultTranslation = new VlogTranslationDetails() { Name = x.Name },
                Translation = x.Translations
                    .Where(t => t.Language == language)
                    .Select(t => new VlogTranslationDetails() { Name = t.Name })
                    .FirstOrDefault()
            };
        }
    }
    
    public class VlogTranslationDetails
    {
        public string Name { get; set; }
    }
}
