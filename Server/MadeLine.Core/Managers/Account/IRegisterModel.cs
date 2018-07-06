namespace MadeLine.Core.Managers
{
    public interface IRegisterModel
    {
        string Email { get; set; }
        
        string Password { get; set; }
                
        string FirstName { get; set; }
        
        string LastName { get; set; }

        string RegisterIP { get; set; }
    }
}
