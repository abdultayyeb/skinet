namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponseModel
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}