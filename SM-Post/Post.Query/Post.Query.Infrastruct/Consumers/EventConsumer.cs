using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using Post.Query.Infrastructure.Converters;
using System.Text.Json;

namespace Post.Query.Infrastructure.Consumers
{
    public class EventConsumer : IEventCunsumer
    {
        private readonly ConsumerConfig _costumerConfig;
        private readonly EventHandler _eventHandler;

        public EventConsumer(IOptions<ConsumerConfig> costumerConfig, EventHandler eventHandler)
        {
            _costumerConfig = costumerConfig.Value;
            _eventHandler = eventHandler;
        }

        public void Consumer(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_costumerConfig)
                    .SetKeyDeserializer(Deserializers.Utf8)
                    .SetValueDeserializer(Deserializers.Utf8)
                    .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumerResult = consumer.Consume();

                if (consumerResult.Message == null) continue;

                var options = new JsonSerializerOptions
                {
                    Converters = { new EventJsonConvert() }
                };

                var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);
                var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

                if (handlerMethod == null)
                {
                    throw new Exception($"Handler for event {@event.GetType()} not found");
                }

                handlerMethod.Invoke(_eventHandler, new object[] { @event });

                consumer.Commit(consumerResult);
            }
        }
    }
}
