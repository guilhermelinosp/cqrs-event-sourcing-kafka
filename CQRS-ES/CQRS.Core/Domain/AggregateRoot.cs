using CQRS.Core.Events;

namespace CQRS.Core.Domain
{
    public abstract class AggregateRoot
    {
        protected Guid _id;
        private readonly List<BaseEvent> _events = new();

        public Guid Id => _id;
        public int Version { get; set; } = -1;
        public IEnumerable<BaseEvent> GetUncomittedEvents => _events;
        public void MarkEventsAsCommitted() => _events.Clear();

        private void ApplyEvent(BaseEvent @event, bool isNew)
        {
            var method = GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            if (method != null)
            {
                method.Invoke(this, new object?[] { @event });

                if (isNew)
                {
                    _events.Add(@event);
                }
            }
            else
            {
                throw new ArgumentNullException($"No Apply method found for event {@event.GetType().Name}");
            }
        }

        protected void RaiseEvent(BaseEvent @event)
        {
            ApplyEvent(@event, true);
        }

        public void ReplayEvent(IEnumerable<BaseEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyEvent(@event, false);
            }
        }
    }
}
