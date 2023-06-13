using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostRemoveEvent : BaseEvent
    {
        public PostRemoveEvent() : base(nameof(PostRemoveEvent))
        {
        }
    }
}
