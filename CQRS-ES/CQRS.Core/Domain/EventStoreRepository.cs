using CQRS.Core.Events;

namespace CQRS.Core.Domain
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public Task SaveAsync(EventModel @event)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
