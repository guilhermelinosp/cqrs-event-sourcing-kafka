namespace CQRS.Core.Messages
{
    public abstract class BaseMessage
    {
        public Guid Id { get; set; }
    }
}
