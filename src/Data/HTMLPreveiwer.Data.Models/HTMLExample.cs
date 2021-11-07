namespace HTMLPreviewer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HTMLPreviewer.Data.Common.Models;

    using static HTMLPreviewer.Data.Common.DataValidation.HTMLExampleValidation;

    public class HTMLExample : BaseModel<string>
    {
        public HTMLExample()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(HTMLContentMaxLength)]
        public string HTMLContent { get; set; }
    }
}
