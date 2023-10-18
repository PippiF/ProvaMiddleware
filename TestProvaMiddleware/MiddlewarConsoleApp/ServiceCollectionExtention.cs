using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewarConsoleApp
{
    public static class ServiceCollectionExtention
    {
        public static void ConfigureMiddle(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyMiddlewareProva>();
        }
    }
}
