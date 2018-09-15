namespace MadeLine.Core.Managers
{
    public interface ICreateSizeModel
    {
        string Name { get; set; }
    }

    public interface IUpdateSizeModel : ICreateSizeModel
    {
        int Id { get; set; }
    }

    public interface ISearchSizeModel
    {
        string Name { get; set; }
    }
}