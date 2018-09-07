namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.ViewModels;
    using MadeLine.Api.ViewModels.Products;
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private IProductManager manager;

        public ProductsController(IOptions<AppSettings> options, IProductManager productManger) : base(options)
        {
            this.manager = productManger;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 201, Type = typeof(OkObjectViewModel<ProductDetailsViewModel>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        public async Task<ActionResult<OkObjectViewModel<ProductDetailsViewModel>>> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.CreateProductAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                .Select(p => new ProductDetailsViewModel() { })
                .FirstOrDefault();
            
            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<ProductDetailsViewModel>("Success", vm);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<ProductDetailsViewModel>))]
        [ProducesResponseType(statusCode: 404, Type = typeof(string))]
        public ActionResult<OkObjectViewModel<ProductDetailsViewModel>> Details(int id)
        {
            var vm = this.manager.GetQueryById(id)
                .Select(p => new ProductDetailsViewModel() { })
                .FirstOrDefault();
            if (vm == null)
            {
                return new NotFoundObjectResult("Product not found");
            }

            return new OkObjectResult(new OkObjectViewModel<ProductDetailsViewModel>("Success", vm));
        }
    }
}