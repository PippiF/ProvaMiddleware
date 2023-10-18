using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;

namespace MiddlewarConsoleApp
{
    public class MyMiddlewareProva
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        public MyMiddlewareProva(RequestDelegate next)
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
        public static IApplicationBuilder UseProvaMidldleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareProva>();
        }
    }
}

