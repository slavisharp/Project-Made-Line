namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using System.Linq;

    public interface ICreateVlogModel
    {
        string Name { get; set; }

        string Alias { get; set; }

        string EmbededVideo { get; set; }

        int ThumbImageId { get; set; }
    }

    public interface IUpdateVlogModel : ICreateVlogModel
    {
        int Id { get; set; }
    }

    public interface ISearchVlogModel
    {
        TranslationLanguage? Language { get; set; }

        string Name { get; set; }

        int Page { get; set; }

        int PageSize { get; set; }
    }
    
    public interface IUpdateTranslationVlogModel
    {
        int Id { get; set; }

        string Name { get; set; }

        TranslationLanguage Language { get; set; }
    }
}
