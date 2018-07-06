namespace MadeLine.Api.Auth
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
