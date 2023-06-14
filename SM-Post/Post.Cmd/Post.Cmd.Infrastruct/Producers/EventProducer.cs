using Confluent.Kafka;
using CQRS.Core.Events;
using CQRS.Core.Producers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Post.Cmd.Infrastructure.Producers
{
    public class EventProducer : IEventProducer
    {
        private readonly ProducerConfig _producerConfig;

        public EventProducer(IOptions<ProducerConfig> config)
        {
            _producerConfig = config.Value;

            var testConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "test-producer",
                Acks = Acks.All
            };

        }

        public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
        {
            using var producer = new ProducerBuilder<string, string>(_producerConfig)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

            var eventMessage = new Message<string, string>()
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Failed to produce message to topic: {deliveryResult.Topic}, partition: {deliveryResult.Partition}, offset: {deliveryResult.Offset}");
            }

            Console.WriteLine($"Event produced to topic: {deliveryResult.Topic}, partition: {deliveryResult.Partition}, offset: {deliveryResult.Offset}");
        }
    }
}
