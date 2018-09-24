namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.ViewModels;
    using MadeLine.Api.ViewModels.Vlogs;
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [Route("api/vlogs")]
    public class VlogsController : BaseController
    {
        private IVlogManager manager;

        public VlogsController(IOptions<AppSettings> options, IVlogManager manager) : base(options)
        {
            this.manager = manager;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(SearchResultModel<VlogDetailsViewModel>))]
        public ActionResult<ISearchResultModel<VlogDetailsViewModel>> Index(ISearchVlogModel search)
        {
            var result = this.manager.SearchVlogs(search);
            var vm = new SearchResultModel<VlogDetailsViewModel>()
            {
                List = result.List.Select(VlogDetailsViewModel.FromEntity(search.Language ?? base.AppSettings.DefaultLanguage)),
                Page = result.Page,
                PageCount = result.PageCount,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
            return vm;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 200, Type = typeof(VlogDetailsViewModel))]
        public ActionResult<VlogDetailsViewModel> Details(int id, TranslationLanguage? language = null)
        {
            var vm = this.manager.GetQueryById(id)
                .Select(VlogDetailsViewModel.FromEntity(language ?? base.AppSettings.DefaultLanguage))
                .FirstOrDefault();

            return vm;
        }
        
        [HttpPost]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 201, Type = typeof(OkObjectViewModel<VlogDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<VlogDetailsViewModel>>> Create(CreateVlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.CreateVlogAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                .Select(VlogDetailsViewModel.FromEntity(AppSettings.DefaultLanguage))
                .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<VlogDetailsViewModel>("Success", vm);
        }

        [HttpPut]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<VlogDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<VlogDetailsViewModel>>> Update(UpdateVlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.UpdateVlogAsync(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                 .Select(VlogDetailsViewModel.FromEntity(AppSettings.DefaultLanguage))
                 .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<VlogDetailsViewModel>("Success", vm);
        }

        [HttpPut("translations")]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<VlogDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<VlogDetailsViewModel>>> UpdateTrasnlation(UpdateVlogTranslationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var result = await this.manager.UpdateVlogTranslation(model);
            if (!result.Succeeded)
            {
                AddManagerErrorsToModelState(result.Errors);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var vm = this.manager.GetQueryById(result.Model.Id)
                 .Select(VlogDetailsViewModel.FromEntity(model.Language))
                 .FirstOrDefault();

            base.Response.StatusCode = (int)HttpStatusCode.Created;
            return new OkObjectViewModel<VlogDetailsViewModel>("Success", vm);
        }
    }
}