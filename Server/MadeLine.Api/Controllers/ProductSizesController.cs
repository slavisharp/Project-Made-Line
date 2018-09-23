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

    [Route("api/products/sizes")]
    public class ProductSizesController : BaseController
    {
        private IProductSizesManager manager;

        public ProductSizesController(IOptions<AppSettings> options, IProductSizesManager productManger) : base(options)
        {
            this.manager = productManger;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IEnumerable<SizeDetailsViewModel>))]
        public ActionResult<IEnumerable<SizeDetailsViewModel>> Index(ISearchSizeModel search)
        {
            var vm = this.manager.SearchProductSize(search)
                .Select(SizeDetailsViewModel.FromEntity)
                .ToArray();

            return vm;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 200, Type = typeof(SizeDetailsViewModel))]
        public ActionResult<SizeDetailsViewModel> Details(int id)
        {
            var vm = this.manager.GetQueryById(id)
                .Select(SizeDetailsViewModel.FromEntity)
                .FirstOrDefault();

            return vm;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 201, Type = typeof(OkObjectViewModel<SizeDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<SizeDetailsViewModel>>> Create(CreateSizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.CreateProductSizeAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                .Select(SizeDetailsViewModel.FromEntity)
                .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<SizeDetailsViewModel>("Success", vm);
        }

        [HttpPut]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<SizeDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<SizeDetailsViewModel>>> Update(UpdateSizeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.UpdateProductSizeAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                 .Select(SizeDetailsViewModel.FromEntity)
                 .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<SizeDetailsViewModel>("Success", vm);
        }
    }
}