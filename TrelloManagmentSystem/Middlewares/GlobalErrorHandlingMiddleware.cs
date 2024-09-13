using TrelloManagmentSystem.Exceptions;
using TrelloManagmentSystem.ViewModels;

namespace TrelloManagmentSystem.Middlewares
{
    public  class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                string message = "";
                ErrorCode errorCode = ErrorCode.UnKnown;
                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.errorCode;
                }
                var result = ResultViewModel<bool>.Failure(errorCode, message);
                await context.Response.WriteAsJsonAsync(result);
            }
            
        }
    }
}
