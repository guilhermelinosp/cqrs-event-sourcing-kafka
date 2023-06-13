using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostDeleteEvent : BaseEvent
    {
        public PostDeleteEvent() : base(nameof(PostDeleteEvent))
        {
        }
    }
}
