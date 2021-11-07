namespace HTMLPreviewer.Services.Data.Models
{
    public static class ModelValidation
    {
        public static class HTMLExampleValidation
        {
            // Max 5MB size
            public const int HTMLContentMaxLength = 5 * 1024 * 1024;

            public const string HTMLContentLengthError = "The size of the content is more than 5MB.";

            public const string EmptyFieldLengthError = "Please enter the field.";
        }
    }
}
