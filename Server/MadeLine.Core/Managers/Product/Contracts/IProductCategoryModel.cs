namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;

    public interface ICreateCategoryModel
    {
        string Name { get; set; }
    }

    public interface IUpdateCategoryModel : ICreateCategoryModel
    {
        int Id { get; set; }
    }

    public interface ISearchCategoryModel
    {
        TranslationLanguage? Language { get; set; }

        string Name { get; set; }
    }

    public interface ICategoryTranslationModel : IUpdateCategoryModel
    {
        TranslationLanguage Language { get; set; }
    }
}