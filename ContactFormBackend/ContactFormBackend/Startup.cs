using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactFormBackend
{
    public class Startup
    {
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            EmailServerConfiguration config = new EmailServerConfiguration
            {
                SmtpPassword = "gr[JwG<2SmB,?_88",
                SmtpServer = "smtp.gmail.com",
                SmtpUsername = "smash.my.email@gmail.com"
            };

            LoggedInUsers userStorage = new LoggedInUsers();

            services.AddSingleton(config);
            services.AddSingleton(userStorage);
            
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireCors(MyAllowSpecificOrigins);
            });
        }
    }
}