namespace MadeLine.Api.ViewModels.Products
{
    using MadeLine.Data.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class CategoryDetailsViewModel 
    {
        public int Id { get; set; }

        public int ProductsCount { get; set; }

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
                    return this.DefaultLanguageTranslation.Name;
                }
            }
        }

        private CategoryTranslationDetails DefaultLanguageTranslation { get; set; }

        private CategoryTranslationDetails Translation { get; set; }

        public static Expression<Func<Category, CategoryDetailsViewModel>> FromEntity(TranslationLanguage language)
        {
            return x => new CategoryDetailsViewModel()
            {
                Id = x.Id,
                ProductsCount = x.Products.Count,
                DefaultLanguageTranslation = new CategoryTranslationDetails() { Name = x.Name },
                Translation = x.Translations
                    .Where(t => t.Language == language)
                    .Select(t => new CategoryTranslationDetails() { Name = t.Name })
                    .FirstOrDefault()
            };
        }
    }
    
    public class CategoryTranslationDetails
    {
        public string Name { get; set; }
    }
}
