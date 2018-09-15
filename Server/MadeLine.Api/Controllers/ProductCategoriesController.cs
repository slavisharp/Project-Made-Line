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

    [Route("api/products/categories")]
    public class ProductCategoriesController : BaseController
    {
        private IProductCategoriesManager manager;

        public ProductCategoriesController(IOptions<AppSettings> options, IProductCategoriesManager productManger) : base(options)
        {
            this.manager = productManger;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IEnumerable<CategoryDetailsViewModel>))]
        public ActionResult<IEnumerable<CategoryDetailsViewModel>> Index(ISearchCategoryModel search)
        {
            var vm = this.manager.SearchProductCategory(search)
                .Select(CategoryDetailsViewModel.FromEntity(search.Language ?? base.AppSettings.DefaultLanguage))
                .ToArray();

            return vm;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<CategoryDetailsViewModel>))]
        public ActionResult<IEnumerable<CategoryDetailsViewModel>> Details(int id, TranslationLanguage? language = null)
        {
            var vm = this.manager.GetQueryById(id)
                .Select(CategoryDetailsViewModel.FromEntity(language ?? base.AppSettings.DefaultLanguage))
                .ToArray();

            return vm;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 201, Type = typeof(OkObjectViewModel<CategoryDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<CategoryDetailsViewModel>>> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.CreateProductCategoryAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                .Select(CategoryDetailsViewModel.FromEntity(AppSettings.DefaultLanguage))
                .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<CategoryDetailsViewModel>("Success", vm);
        }

        [HttpPut]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<CategoryDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<CategoryDetailsViewModel>>> Update(UpdateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.UpdateProductCategoryAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                 .Select(CategoryDetailsViewModel.FromEntity(AppSettings.DefaultLanguage))
                 .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<CategoryDetailsViewModel>("Success", vm);
        }

        [HttpPut("translations")]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<CategoryDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<CategoryDetailsViewModel>>> UpdateTrasnlation(UpdateCategoryTranslationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.UpdateProductCategoryTranslation(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                 .Select(CategoryDetailsViewModel.FromEntity(model.Language))
                 .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<CategoryDetailsViewModel>("Success", vm);
        }
    }
}