namespace WebApplication

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

type Startup() =

    member this.ConfigureServices(services: IServiceCollection) =
        ()

    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseRouting() |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapGet("/", fun context -> context.Response.WriteAsync("Get!")) |> ignore
            endpoints.MapPost("/", fun context -> context.Response.WriteAsync("Post!")) |> ignore
            endpoints.MapPut("/", fun context -> context.Response.WriteAsync("Put!")) |> ignore
            endpoints.MapDelete("/", fun context -> context.Response.WriteAsync("Delete!")) |> ignore
            ) |> ignore
