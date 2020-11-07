using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRAndWorkerServicePart2
{
    public sealed class MessageBrokerHub : Hub
    {

        public Task CommandReceived(string lightColor, bool state)
        {
            return Task.CompletedTask;
        }
    }
}
