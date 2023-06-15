namespace CQRS.Core.Consumers
{
    public interface IEventCunsumer
    {
        void Consumer(string topic);
    }
}
