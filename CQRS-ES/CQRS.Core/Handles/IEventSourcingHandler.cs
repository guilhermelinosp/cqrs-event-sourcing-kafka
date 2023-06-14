using CQRS.Core.Domain;

namespace CQRS.Core.Handles
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AggregateRoot aggregate);
        Task<T> GetByIdAsync(Guid id);
    }
}
