using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                    await streamWriter.WriteLineAsync($"{context.Request.Scheme}{context.Request.Host}{context.Request.Path}<{context.Request.Body.Position}>[{context.Request.QueryString.Value}] executed at [{DateTime.UtcNow}]");
                }
            }
            catch(Exception ex)
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
