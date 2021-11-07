namespace HTMLPreviewer.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HTMLPreviewer.Data.Models;
    using HTMLPreviewer.Services.Mapping;

    using static HTMLPreviewer.Services.Data.Models.ModelValidation.HTMLExampleValidation;

    public class HTMLExampleEditModel : IMapFrom<HTMLExample>
    {
        public string Id { get; set; }


        [Required(ErrorMessage = EmptyFieldLengthError)]
        [MaxLength(HTMLContentMaxLength, ErrorMessage = HTMLContentLengthError)]
        public string HTMLContent { get; set; }
    }
}
