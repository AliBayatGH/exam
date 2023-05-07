using Exam.Infrastructure.Logger;
using Exam.Infrastructure.Wrapper;

namespace Exam.API.MiddleWare;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Logger.Log.Error(ex.Message + Environment.NewLine + ex.StackTrace);
            
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new TResponse<string>("مشکلی در سرویس پیش آمده لطفا بعدا تلاش فرمایید").ToString());
        }
    }
}