using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SignalRAndWorkerServicePart2
{
    internal abstract class PublisherCommandMessageBase : IDisposable
    {
        private readonly JsonStringEnumConverter _jsonStringEnumConverter = new JsonStringEnumConverter(namingPolicy: default, allowIntegerValues: false);
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        protected PublisherCommandMessageBase()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonSerializerOptions.Converters.Add(_jsonStringEnumConverter);
        }

        public Task Publish(CommandMessage commandMessage)
        {
            return PublishCore(commandMessage);
        }

        protected byte[] SerializeMessage<T>(T message)
        {
            string messageJson = JsonSerializer.Serialize<T>(message, _jsonSerializerOptions);
            return Encoding.UTF8.GetBytes(messageJson);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract Task PublishCore(CommandMessage commandMessage);
        protected abstract void Dispose(bool disposing);
    }
}
