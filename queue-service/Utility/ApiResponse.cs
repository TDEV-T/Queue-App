namespace QueueService.Utility
{

        public class ApiResponse<T>
        {
            public bool IsSuccess { get; private set; }
            public int StatusCode { get; private set; }
            public string? Message { get; private set; }
            public T? Data { get; private set; }

            public static ApiResponse<T> Success(T data, string message = "Operation completed successfully.", int statusCode = 200)
            {
                return new ApiResponse<T>
                {
                    IsSuccess = true,
                    StatusCode = statusCode,
                    Message = message,
                    Data = data
                };
            }

            public static ApiResponse<T> Fail(string message, int statusCode = 400)
            {
                return new ApiResponse<T>
                {
                    IsSuccess = false,
                    StatusCode = statusCode,
                    Message = message,
                    Data = default(T) 
                };
            }
        }

}
