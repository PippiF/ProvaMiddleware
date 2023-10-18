using Microsoft.AspNetCore.Http;
using MiddlewarConsoleApp;
using Serilog;

namespace TestMiddleware
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Category("TEST")]
        public void TestMiddlewareExecution()
        {
            // Configura il logger per il test
            Log.Logger = new LoggerConfiguration()
                                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day) // Log su file
                                .CreateLogger();

            // Crea un oggetto HttpContext fittizio per il test
            var context = new DefaultHttpContext();

            // Crea un delegato RequestDelegate fittizio
            RequestDelegate next = (c) =>
            {
                // Simula la gestione della richiesta successiva
                Log.Information("Next middleware executed.");
                return Task.CompletedTask;
            };

            // Crea un'istanza del middleware
            var middleware = new MyMiddlewareProva(next);

            // Esegui il middleware
            middleware.Invoke(context).Wait(); // Assicurati che il tuo metodo Invoke sia asincrono

            // Fai le asserzioni necessarie
            // Ad esempio, verifica se il logger ha registrato i messaggi correttamente
            // Verifica che il middleware abbia eseguito correttamente la sua logica

            // Esegui le asserzioni NUnit
            Assert.That(context.Response.StatusCode, Is.EqualTo(200)); // Adatta la condizione al tuo scenario
        }
    }
}