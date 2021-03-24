using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared;

namespace Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:3600/Test")
                .WithAutomaticReconnect()
                .AddMessagePackProtocol()
                .Build();

            await hubConnection.StartAsync();

            hubConnection.On<TheMessage>("ReceiveMessage",
                theMessage =>
                {
                    Console.WriteLine("Received message:");
                    Console.WriteLine(JsonSerializer.Serialize(theMessage));
                });

            var message = new TheMessage
            {
                Text = "Test",
                Status = 1
            };

            while (Console.ReadKey().Key != ConsoleKey.Escape)
                await hubConnection.SendAsync("SendMessage", message);

            await hubConnection.DisposeAsync();
        }
    }
}
