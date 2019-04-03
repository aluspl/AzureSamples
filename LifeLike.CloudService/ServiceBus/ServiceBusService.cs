using LifeLike.Shared.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LifeLike.CloudService.ServiceBus
{
    public class ServiceBusService : IHostedService
    {
        private readonly IConfiguration _config;
        private readonly QueueClient _client;

        public ServiceBusService(IConfiguration configuration)
        {
            _config = configuration;
            _client = GetClient(_config["QueueName"]);
        }

        public void Subscribe(string name, Func<string> listener)
        {
          
        }
        private QueueClient GetClient(string queue)
        {
            return new QueueClient(_config["SB"], queue, ReceiveMode.PeekLock);
        }

        public void SendMessage(string message, string name)
        {
           //_client.SendAsync(new Message { Body = Encoding.UTF8.GetBytes(message) }).ConfigureAwait(false).GetAwaiter();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
         //   _client.RegisterMessageHandler(Subscribe, OnError);        
        }

        private Task OnError(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            return Task.CompletedTask;
        }

        private Task Subscribe(Message arg1, CancellationToken arg2)
        {
            Console.WriteLine($"Message handler encountered an exception {arg1.Body}.");
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
      //      await _client.CloseAsync();
        }
    }
}
