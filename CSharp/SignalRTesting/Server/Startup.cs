using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddCors(cors => cors.AddDefaultPolicy(policy =>
            //     policy
            //         .AllowAnyHeader()
            //         .AllowAnyMethod()
            //         .AllowCredentials()
            //         .SetIsOriginAllowed(_ => true)
            // ));

            services.AddSignalR()
                .AddMessagePackProtocol();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // app.UseCors();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", httpContext =>
                {
                    httpContext.Response.Redirect("/client.html");
                    return Task.CompletedTask;
                });
                endpoints.MapHub<TestHub>("/test");
            });
        }
    }
}
