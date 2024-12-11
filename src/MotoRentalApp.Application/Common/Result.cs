namespace MotoRentalApp.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        
        public static Result<T> Success(T data)
        {
            return new Result<T> { IsSuccess = true, Data = data };
        }

        
        public static Result<T> Failure(string errorMessage, int? statusCode = null)
        {
            return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
        }

        
        public static Result<T> Success()
        {
            return new Result<T> { IsSuccess = true };
        }
    }

    
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        public static Result Success()
        {
            return new Result { IsSuccess = true };
        }

        public static Result Failure(string errorMessage, int? statusCode = null)
        {
            return new Result { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
        }
    }
}
