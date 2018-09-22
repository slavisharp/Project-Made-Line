namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.ViewModels;
    using MadeLine.Api.ViewModels.Images;
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    [Route("api/images")]
    public class ImagesController : BaseController
    {
        private IImageManager manager;

        public ImagesController(IOptions<AppSettings> options, IImageManager imageManager, IHostingEnvironment env) : base(options)
        {
            this.manager = imageManager;
        }

        [HttpPost("")]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        [ProducesResponseType(statusCode: 201, Type = typeof(OkObjectViewModel<ImageDetailsViewModel>))]
        public async Task<ActionResult<OkObjectViewModel<ImageDetailsViewModel>>> Upload(IFormFile image)
        {
            if (image == null)
            {
                ModelState.AddModelError(string.Empty, "No image sent to server!");
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            try
            {
                var entity = await this.manager.SaveImageAsync(image);
                var vm = new ImageDetailsViewModel(entity);
                vm.Url = $"{base.AppSettings.URLS.BaseURL}{vm.Url}";
                return new OkObjectViewModel<ImageDetailsViewModel>("Image uploaded!", vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }
        }
    }
}