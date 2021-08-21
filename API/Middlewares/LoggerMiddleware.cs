using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            object locker = new object();
            bool lockWasTaken = false;
            try
            {
                System.Threading.Monitor.Enter(locker, ref lockWasTaken);
                using (var streamWriter = new StreamWriter("requests.log", true))
                {
                    await streamWriter
                        .WriteLineAsync($"{context.Request.Scheme}{context.Request.Host}{context.Request.Path}<{context.Request.Body.Position}>[{context.Request.QueryString.Value}] executed at [{DateTime.UtcNow}]");
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

            }
            finally
            {
                System.Threading.Monitor.Exit(locker);
            }
            await _next(context);
        }
    }
}
