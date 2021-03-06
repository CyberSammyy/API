using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using WebSocketChatCoreLib;
using WebSocketChatCoreLib.Commands;
using WebSocketChatCoreLib.Interfaces;
using WebSocketChatCoreLib.Models;
using WebSocketChatServer;

namespace WebSocketChatServerApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            services.AddScoped<SocketHandler, WebSocketMessageHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<CommandHelper>();

            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(UserModelProfile))
            };

            services.AddAutoMapper(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();


            app.Map("/ws", x => x.UseMiddleware<SocketMiddleware>(services.GetService<SocketHandler>()));
            app.UseStaticFiles();
        }
    }
}
