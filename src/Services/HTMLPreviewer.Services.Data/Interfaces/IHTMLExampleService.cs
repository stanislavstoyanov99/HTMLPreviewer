namespace HTMLPreviewer.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using HTMLPreviewer.Services.Data.Models;

    public interface IHTMLExampleService
    {
        Task<string> CreateAsync(HTMLExampleCreateModel htmlExampleCreateModel);

        Task<bool> EditAsync(HTMLExampleEditModel htmlExampleEditModel);

        Task<bool> CheckWithOriginalAsync(HTMLExampleCheckWithOriginalInputModel htmlExampleCheckWithOriginalModel);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);
    }
}
