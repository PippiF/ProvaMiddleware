using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Net;
//using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TestProvaMiddleware.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information("::::::MyMiddleware executing");

            try
            {
                //_logger.LogInformation("MyMiddleware executing..");
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            //_logger.LogError(ex.Message + ":::::::::::SONO QUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            //Log.Error(ex.Message + ":::::::::::SONO QUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            var errorid = Activity.Current?.Id ?? context.TraceIdentifier;
            var customerError = $"ErrorId-{errorid}: Message-Some kind of error happened in the API";
            var resul = JsonConvert.SerializeObject(customerError);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(resul);

        }



        //private static void Logweberror()
        //{
        //    AppLogger.WriteError("Ciao");
        //}
    }

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
