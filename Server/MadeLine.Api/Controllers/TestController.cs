namespace MadeLine.Api.Controllers
{
    using MadeLine.Core.Settings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("api/[controller]")]
    public class TestController : BaseController
    {
        public TestController(IOptions<AppSettings> options): base(options)
        {

        }

        [HttpGet("test-user")]
        [Authorize(Policy = StaticVariables.JWT_UserRole_Policy)]
        public ActionResult<string> TestUser()
        {
            return "success for User";
        }

        [HttpGet("test-brand")]
        [Authorize(Policy = StaticVariables.JWT_BrandRole_Policy)]
        public ActionResult<string> TestBrandUser()
        {
            return "success for Brand User";
        }

        [HttpGet("test-admin")]
        [Authorize(Policy = StaticVariables.JWT_AdminRole_Policy)]
        public ActionResult<string> TestAdmin()
        {
            return "success for Admin";
        }
    }
}