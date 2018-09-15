namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.ViewModels;
    using MadeLine.Api.ViewModels.Products;
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [Route("api/products/colors")]
    public class ProductColorsController : BaseController
    {
        private IProductColorsManager manager;

        public ProductColorsController(IOptions<AppSettings> options, IProductColorsManager productManger) : base(options)
        {
            this.manager = productManger;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IEnumerable<ProductColorDetailsViewModel>))]
        public ActionResult<IEnumerable<ProductColorDetailsViewModel>> Index(ISearchColorModel search)
        {
            var vm = this.manager.SearchProductColor(search)
                .Select(ProductColorDetailsViewModel.FromEntity)
                .ToArray();

            return vm;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<ProductColorDetailsViewModel>))]
        public ActionResult<IEnumerable<ProductColorDetailsViewModel>> Details(int id)
        {
            var vm = this.manager.GetQueryById(id)
                .Select(ProductColorDetailsViewModel.FromEntity)
                .ToArray();

            return vm;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 201, Type = typeof(OkObjectViewModel<ProductColorDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<ProductColorDetailsViewModel>>> Create(CreateProductColorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.CreateProductColorAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                .Select(ProductColorDetailsViewModel.FromEntity)
                .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<ProductColorDetailsViewModel>("Success", vm);
        }

        [HttpPut]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<ProductColorDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<ProductColorDetailsViewModel>>> Update(UpdateProductColorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.UpdateProductColorAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                 .Select(ProductColorDetailsViewModel.FromEntity)
                 .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<ProductColorDetailsViewModel>("Success", vm);
        }
    }
}