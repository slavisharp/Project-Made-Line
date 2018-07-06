namespace MadeLine.Data.Models
{
    public interface IKeyEntity<K>
    {
        K Id { get; set; }
    }
}
