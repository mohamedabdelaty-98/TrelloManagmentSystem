using TrelloManagmentSystem.Data;

namespace TrelloManagmentSystem.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Context context;

        public TransactionMiddleware(RequestDelegate next, Context context)
        {
            this.next = next;
            this.context = context;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var method=httpContext.Request.Method;
            if (method.ToUpper() != "GET")
            {
                var transaction=context.Database.BeginTransaction();
                try
                {
                    await next(httpContext);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
 
        }
    }
}
