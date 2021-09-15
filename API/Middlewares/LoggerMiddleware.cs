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
                    streamWriter
                        .WriteLine($"{context.Request.Scheme}{context.Request.Host}{context.Request.Path}[{context.Request.QueryString.Value}] executed at [{DateTime.UtcNow}]");
                }
            }
            catch (Exception ex)
            {
                using (var streamWriter = new StreamWriter("exceptions.log", true))
                {
                    streamWriter
                        .WriteLine($"EXCEPTION thrown at {DateTime.UtcNow:G} while performing: {context.Request} request. \r\n {ex.Message}. \r\n Inner text: {ex.InnerException.Message}");
                }
            }
            finally
            {
                System.Threading.Monitor.Exit(locker);
            }
            await _next(context);
        }
    }
}
