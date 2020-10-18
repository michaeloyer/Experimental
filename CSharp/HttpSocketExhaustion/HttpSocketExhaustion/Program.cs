using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HttpSocketExhaustion
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var host = GetWebHost();
            await host.StartAsync();
            Console.WriteLine("Starting connections");
            
            // BAD
            for (var i = 0; i < 10; i++)
            {
                using var client = new HttpClient();
                var result = await client.GetAsync("https://localhost:50001");
                Console.WriteLine(result.StatusCode);
            }

            // BETTER
            {
                using var globalClient = new HttpClient();
                for (var i = 0; i < 10; i++)
                {
                    var result = await globalClient.GetAsync("https://localhost:50002");
                    Console.WriteLine(result.StatusCode);
                }
            }
            
            // BEST without the use of an HTTP Client Factory
            {
                using var messsageHandler = new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                    UseCookies = false
                };

                HttpClient GetHttpClient() => new HttpClient(messsageHandler, false);
                for (var i = 0; i < 10; i++)
                {
                    using var client = GetHttpClient();
                    var result = await client.GetAsync("https://localhost:50003");
                    Console.WriteLine(result.StatusCode);
                }
            }

            Console.WriteLine("Connections done");
            Console.WriteLine("Check 'netstat' and grep for 50001, 50002, and 50003");
            await host.StopAsync();
        }

        private static IHost GetWebHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHost(config =>
                    config
                        .UseKestrel()
                        .UseUrls("https://localhost:50001", "https://localhost:50002", "https://localhost:50003")
                        .ConfigureServices(services => services.AddRouting())
                        .Configure(app =>
                        {
                            app.UseRouting();
                            app.UseEndpoints(endpoints =>
                            {
                                endpoints.MapGet("/", async context => await context.Response.WriteAsync("OK"));
                            });
                        }))
                .Build();
        }
    }
}