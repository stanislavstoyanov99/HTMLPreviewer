namespace HTMLPreviewer.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using HTMLPreviewer.Data;
    using HTMLPreviewer.Data.Models;
    using HTMLPreviewer.Services.Data.Common;
    using HTMLPreviewer.Services.Data.Interfaces;
    using HTMLPreviewer.Services.Data.Models;
    using HTMLPreviewer.Services.Mapping;

    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;
    using Xunit;

    public class HTMLExampleServiceTests : IDisposable
    {
        private readonly IHTMLExampleService htmlExampleService;
        private HTMLPreviewerDbContext dbContext;
        private SqliteConnection connection;

        private HTMLExample firstHtmlExample;

        public HTMLExampleServiceTests()
        {
            this.InitializeMapper();
            this.InitializeDatabase();
            this.InitializeFields();

            this.htmlExampleService = new HTMLExampleService(this.dbContext);
        }

        [Fact]
        public async Task TestAddingHTMLExample()
        {
            var model = new HTMLExampleCreateModel
            {
                HTMLContent = "<h1>I am test html example</h1>",
            };

            await this.htmlExampleService.CreateAsync(model);
            var count = await this.dbContext.HTMLExamples.CountAsync();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task CheckSettingOfHTMLExampleProperties()
        {
            var model = new HTMLExampleCreateModel
            {
                HTMLContent = "<p>Hello world!</p>",
            };

            var htmlExampleId = await this.htmlExampleService.CreateAsync(model);

            var htmlExample = await this.dbContext.HTMLExamples.FirstOrDefaultAsync();

            Assert.Equal(htmlExample.Id, htmlExampleId);
            Assert.Equal(htmlExample.HTMLContent, model.HTMLContent);
        }

        [Fact]
        public async Task CheckIfEditingHTMLExampleWorksCorrectly()
        {
            await this.SeedHTMLExamples();

            var htmlExampleEditViewModel = new HTMLExampleEditModel
            {
                Id = this.firstHtmlExample.Id,
                HTMLContent = "<p>Changed HTML content</p>",
            };

            await this.htmlExampleService.EditAsync(htmlExampleEditViewModel);

            Assert.Equal(htmlExampleEditViewModel.HTMLContent, this.firstHtmlExample.HTMLContent);
        }

        [Fact]
        public async Task CheckIfEditingHTMLExampleReturnsNullReferenceException()
        {
            await this.SeedHTMLExamples();

            var htmlExampleEditViewModel = new HTMLExampleEditModel
            {
                Id = Guid.NewGuid().ToString(),
            };

            var exception = await Assert
                .ThrowsAsync<NullReferenceException>(async () => await this.htmlExampleService.EditAsync(htmlExampleEditViewModel));

            Assert.Equal(
                string.Format(ExceptionMessages.HTMLExampleNotFound, htmlExampleEditViewModel.Id), exception.Message);
        }

        [Fact]
        public async Task CheckIfGetViewModelByIdAsyncWorksCorrectly()
        {
            await this.SeedHTMLExamples();

            var expectedModel = new HTMLExampleDetailsModel
            {
                Id = this.firstHtmlExample.Id,
                HTMLContent = this.firstHtmlExample.HTMLContent,
            };

            var viewModel = await this.htmlExampleService
                .GetViewModelByIdAsync<HTMLExampleDetailsModel>(this.firstHtmlExample.Id);

            var expectedObj = JsonConvert.SerializeObject(expectedModel);
            var actualResultObj = JsonConvert.SerializeObject(viewModel);

            Assert.Equal(expectedObj, actualResultObj);
        }

        [Fact]
        public async Task CheckIfGetViewModelByIdAsyncThrowsNullReferenceException()
        {
            await this.SeedHTMLExamples();

            var id = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<NullReferenceException>(async () =>
                    await this.htmlExampleService.GetViewModelByIdAsync<HTMLExampleDetailsModel>(id));

            Assert.Equal(string.Format(ExceptionMessages.HTMLExampleNotFound, id), exception.Message);
        }

        [Fact]
        public async Task CheckIfCheckWithOriginalAsyncReturnsTrue()
        {
            await this.SeedHTMLExamples();

            var model = new HTMLExampleCheckWithOriginalInputModel()
            {
                Id = this.firstHtmlExample.Id,
                CurrentHtmlContent = this.firstHtmlExample.HTMLContent,
            };

            var isOriginal = await this.htmlExampleService.CheckWithOriginalAsync(model);

            Assert.True(isOriginal);
        }

        [Fact]
        public async Task CheckIfCheckWithOriginalAsyncReturnsFalse()
        {
            await this.SeedHTMLExamples();

            var model = new HTMLExampleCheckWithOriginalInputModel()
            {
                Id = this.firstHtmlExample.Id,
                CurrentHtmlContent = "<p>Another HTML content</p>",
            };

            var isOriginal = await this.htmlExampleService.CheckWithOriginalAsync(model);

            Assert.False(isOriginal);
        }

        [Fact]
        public async Task CheckIfCheckWithOriginalAsyncThrowsNullReferenceException()
        {
            await this.SeedHTMLExamples();

            var model = new HTMLExampleCheckWithOriginalInputModel()
            {
                Id = Guid.NewGuid().ToString(),
                CurrentHtmlContent = this.firstHtmlExample.HTMLContent,
            };

            var exception = await Assert
                .ThrowsAsync<NullReferenceException>(async () =>
                    await this.htmlExampleService.CheckWithOriginalAsync(model));

            Assert.Equal(string.Format(ExceptionMessages.HTMLExampleNotFound, model.Id), exception.Message);
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }

        private async Task SeedHTMLExamples()
        {
            await this.dbContext.HTMLExamples.AddAsync(this.firstHtmlExample);

            await this.dbContext.SaveChangesAsync();
        }

        private void InitializeFields()
        {
            this.firstHtmlExample = new HTMLExample
            {
                HTMLContent = "<p>Test HTML Example</p>",
            };
        }

        private void InitializeDatabase()
        {
            this.connection = new SqliteConnection("DataSource=:memory:");
            this.connection.Open();

            var options = new DbContextOptionsBuilder<HTMLPreviewerDbContext>().UseSqlite(this.connection);
            this.dbContext = new HTMLPreviewerDbContext(options.Options);

            this.dbContext.Database.EnsureCreated();
        }

        private void InitializeMapper() => AutoMapperConfig.
            RegisterMappings(Assembly.Load("HTMLPreviewer.Services.Data.Models"));
    }
}
