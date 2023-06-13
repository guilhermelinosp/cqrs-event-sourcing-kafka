using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostLikeEvent : BaseEvent
    {
        public PostLikeEvent() : base(nameof(PostLikeEvent))
        {
        }


    }
}
