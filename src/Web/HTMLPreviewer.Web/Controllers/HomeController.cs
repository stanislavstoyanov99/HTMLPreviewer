namespace HTMLPreviewer.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using HTMLPreviewer.Web.Models;
    using HTMLPreviewer.Services.Data.Models;
    using HTMLPreviewer.Services.Data.Interfaces;

    using AspNetCoreHero.ToastNotification.Abstractions;

    public class HomeController : Controller
    {
        private readonly IHTMLExampleService htmlExampleService;
        private readonly INotyfService notyfService;

        public HomeController(IHTMLExampleService htmlExampleService, INotyfService notyfService)
        {
            this.htmlExampleService = htmlExampleService;
            this.notyfService = notyfService;
        }

        public IActionResult Index()
        {
            var model = new HTMLExampleCreateModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HTMLExampleCreateModel htmlExampleCreateModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("~/Views/Home/Index.cshtml", htmlExampleCreateModel);
            }

            var htmlExampleId = await this.htmlExampleService
                .CreateAsync(htmlExampleCreateModel);

            htmlExampleCreateModel.Id = htmlExampleId;

            this.notyfService.Success("Successfully Saved.");

            return this.View("~/Views/Home/Index.cshtml", htmlExampleCreateModel);
        }

        public async Task<IActionResult> Preview(string id)
        {
            try
            {
                var htmlExample = await this.htmlExampleService
                    .GetViewModelByIdAsync<HTMLExampleDetailsModel>(id);

                return this.View(htmlExample);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotFoundPage(HttpErrorViewModel errorViewModel)
        {
            if (errorViewModel.StatusCode == 404)
            {
                return this.View(errorViewModel);
            }

            return this.View(
                "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
