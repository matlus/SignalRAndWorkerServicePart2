using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRAndWorkerServicePart2
{
    public sealed class MessageBrokerPubSubWorker : BackgroundService
    {
        private readonly SignalProcessorManager _signalProcessorManager = new SignalProcessorManager();
        private readonly IHubContext<MessageBrokerHub> _messageBrokerHubContext;

        public MessageBrokerPubSubWorker(IHubContext<MessageBrokerHub> messageBrokerHubContext)
        {
            _messageBrokerHubContext = messageBrokerHubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            await _signalProcessorManager.StartListening(async eventMessage =>
            {
                await _messageBrokerHubContext.Clients.All.SendAsync("onMessageReceived", eventMessage, stoppingToken);
            });
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _signalProcessorManager.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
