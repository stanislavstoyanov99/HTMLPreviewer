namespace HTMLPreviewer.Services.Data
{
    using System;
    using System.Web;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;

    using Microsoft.EntityFrameworkCore;

    using HTMLPreviewer.Data;
    using HTMLPreviewer.Data.Models;
    using HTMLPreviewer.Services.Data.Common;
    using HTMLPreviewer.Services.Data.Interfaces;
    using HTMLPreviewer.Services.Data.Models;
    using HTMLPreviewer.Services.Mapping;

    public class HTMLExampleService : IHTMLExampleService
    {
        private readonly HTMLPreviewerDbContext dbContext;

        public HTMLExampleService(HTMLPreviewerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAsync(HTMLExampleCreateModel htmlExampleCreateModel)
        {
            var htmlExample = new HTMLExample
            {
                HTMLContent = HttpUtility.HtmlDecode(htmlExampleCreateModel.HTMLContent),
            };

            await this.dbContext.AddAsync(htmlExample);
            await this.dbContext.SaveChangesAsync();

            return htmlExample.Id;
        }

        public async Task<bool> EditAsync(HTMLExampleEditModel htmlExampleEditModel)
        {
            var htmlExample = await this.dbContext
                .HTMLExamples
                .FirstOrDefaultAsync(x => x.Id == htmlExampleEditModel.Id);

            if (htmlExample == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.HTMLExampleNotFound, htmlExampleEditModel.Id));
            }

            htmlExample.HTMLContent = HttpUtility.HtmlDecode(htmlExampleEditModel.HTMLContent);

            this.dbContext.HTMLExamples.Update(htmlExample);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CheckWithOriginalAsync(HTMLExampleCheckWithOriginalInputModel model)
        {
            var originalExample = await dbContext.HTMLExamples
                .Select(x => new HTMLExampleCheckWithOriginalDetailsModel
                {
                    Id = x.Id,
                    HTMLContent = x.HTMLContent,
                })
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (originalExample == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.HTMLExampleNotFound, model.Id));
            }

            var normalizedOriginalContent = Regex.Replace(originalExample.HTMLContent, @"\s", "");

            var decodedCurrentContent = HttpUtility.HtmlDecode(model.CurrentHtmlContent);
            var normalizedCurrentContent = Regex.Replace(decodedCurrentContent, @"\s", "");

            return string.Equals(
                normalizedOriginalContent,
                normalizedCurrentContent,
                StringComparison.OrdinalIgnoreCase);
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var htmlExampleViewModel = await this.dbContext
                .HTMLExamples
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            if (htmlExampleViewModel == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.HTMLExampleNotFound, id));
            }

            return htmlExampleViewModel;
        }
    }
}
