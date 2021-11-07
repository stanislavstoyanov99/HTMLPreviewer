namespace HTMLPreviewer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HTMLPreviewer.Services.Data.Interfaces;
    using HTMLPreviewer.Services.Data.Models;

    using Microsoft.AspNetCore.Mvc;

    public class HTMLExamplesController : ApiController
    {
        private readonly IHTMLExampleService htmlExampleService;

        public HTMLExamplesController(IHTMLExampleService htmlExampleService)
        {
            this.htmlExampleService = htmlExampleService;
        }

        [Route(nameof(Run))]
        [HttpPost]
        public ActionResult<HTMLExampleCreateModel> Run(HTMLExampleCreateModel htmlExampleCreateModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            return htmlExampleCreateModel;
        }

        [Route(nameof(CheckWithOriginal))]
        [HttpPost]
        public async Task<ActionResult> CheckWithOriginal(HTMLExampleCheckWithOriginalInputModel model)
        {
            try
            {
                var isOriginal = await this.htmlExampleService
                    .CheckWithOriginalAsync(model);

                var jsonResult = new JsonResult(isOriginal);

                return jsonResult;
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route(nameof(Edit))]
        [HttpPost]
        public async Task<ActionResult> Edit(HTMLExampleEditModel htmlExampleEditModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            try
            {
                var isEdited = await this.htmlExampleService
                    .EditAsync(htmlExampleEditModel);

                var jsonResult = new JsonResult(isEdited);

                return jsonResult;
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
