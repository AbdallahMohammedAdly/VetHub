namespace VetHub02.Errors
{
    public class ApivalidationError:ApiError
    {
        public IEnumerable<string> Errors { get; set; }

        public ApivalidationError():base(400)
        {
            Errors = new List<string>();
        }
    }
}
