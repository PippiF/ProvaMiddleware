using MiddlewarConsoleApp;
using TestProvaMiddleware.Middleware;

namespace TestProvaMiddleware
{
    public static class ServiceCollectionExtention
    {
        public static void ConfigureMiddl(this IApplicationBuilder next)
        {
            next.ConfigureMiddle();
        }

    }
}
