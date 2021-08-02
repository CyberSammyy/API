using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Classes;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<UsersDBContext>(options
                => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //var smtpOptions = Configuration.GetSection("SmtpOptions");
            //services.Configure<SmtpOptions>(smtpOptions);

            var hashSettings = Configuration.GetSection("HashSettings");
            services.Configure<HashSettings>(hashSettings);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            services.RegisterServices();

            services.AddControllers();

            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(UserProfile))
            };
            services.AddAutoMapper(assemblies);

            services.AddAuthentication(appSettings);
            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
