namespace VetHub02.Errors
{
    public class ApiError
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiError(int statusCode,string? message = null) 
        {
            this.StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(StatusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {

                400 => "A Bad Request , You Have Made",
                401 => "Authorized , You Are Not",
                404 => "Not Found",
                405 => "You did not send any data",
                500 => "there is srver Error",
                _  => null

            };
        }
    }
}
