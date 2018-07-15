namespace MadeLine.Api.Helpers
{
    using MadeLine.Api.Auth;
    using MadeLine.Api.ViewModels.Accounts;
    using MadeLine.Core.Settings;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public static class Token
    {
        public static async Task<ResponseTokenViewModel> GenerateJwt(
            ClaimsIdentity identity, 
            IJwtFactory jwtFactory, 
            string userName, 
            JwtIssuerOptions jwtOptions, 
            JsonSerializerSettings serializerSettings)
        {
            var response = new ResponseTokenViewModel
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                Token = await jwtFactory.GenerateEncodedToken(userName, identity),
                Expires = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return response;
        }
    }
}
