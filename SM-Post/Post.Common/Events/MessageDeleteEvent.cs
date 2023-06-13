using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class MessageDeleteEvent : BaseEvent
    {
        public MessageDeleteEvent() : base(nameof(MessageDeleteEvent))
        {
        }

        public string? Message { get; set; }
    }
}
