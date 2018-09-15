namespace MadeLine.Core.Managers
{
    public interface ICreateColorModel
    {
        string Name { get; set; }

        string Value { get; set; }
    }

    public interface IUpdateColorModel : ICreateColorModel
    {
        int Id { get; set; }
    }

    public interface ISearchColorModel
    {
        string Name { get; set; }

        string Value { get; set; }
    }
}