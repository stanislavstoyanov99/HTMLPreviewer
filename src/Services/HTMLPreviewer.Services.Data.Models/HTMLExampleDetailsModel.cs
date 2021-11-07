namespace HTMLPreviewer.Services.Data.Models
{
    using Ganss.XSS;

    using HTMLPreviewer.Data.Models;
    using HTMLPreviewer.Services.Mapping;

    public class HTMLExampleDetailsModel : IMapFrom<HTMLExample>
    {
        public string Id { get; set; }

        public string HTMLContent { get; set; }

        public string SanitizedHTMLContent => new HtmlSanitizer().Sanitize(this.HTMLContent);
    }
}
