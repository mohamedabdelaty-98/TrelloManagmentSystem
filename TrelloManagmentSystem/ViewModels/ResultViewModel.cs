namespace TrelloManagmentSystem.ViewModels
{
    public class ResultViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public ErrorCode errorCode { get; set; }

     


        public static ResultViewModel<T> Success(T data, string message="")
        {
            return new ResultViewModel<T>
            {
                Data = data,
                Message = message,
                errorCode = ErrorCode.None,
                IsSuccess = true
            };         
        }

        public static ResultViewModel<T> Failure(ErrorCode errorCode, string message)
        {
            return new ResultViewModel<T>
            {
                Data = default,
                Message = message,
                errorCode = errorCode,
                IsSuccess = false
            };
        }

    }



    public class ResultViewModel
    {

        public bool IsSuccess { get; set; } = true;
        public bool IsFailure => !IsSuccess;
        public ErrorCode errorCode { get; set; } = default!;


        public ResultViewModel(bool isSuccess, ErrorCode errorCode)
        {
            IsSuccess = isSuccess;
            errorCode = errorCode;
        }

        public static ResultViewModel Success() => new(true, ErrorCode.None);
        public static ResultViewModel Failure(ErrorCode errorCode) => new(false, errorCode);

    }

    }
