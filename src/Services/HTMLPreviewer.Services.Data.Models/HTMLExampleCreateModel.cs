namespace HTMLPreviewer.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static HTMLPreviewer.Services.Data.Models.ModelValidation.HTMLExampleValidation;

    public class HTMLExampleCreateModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = EmptyFieldLengthError)]
        [MaxLength(HTMLContentMaxLength, ErrorMessage = HTMLContentLengthError)]
        public string HTMLContent { get; set; } 
    }
}
