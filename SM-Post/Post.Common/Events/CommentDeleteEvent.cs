using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class CommentDeleteEvent : BaseEvent
    {
        public CommentDeleteEvent() : base(nameof(CommentDeleteEvent))
        {
        }

        public Guid CommnetId { get; set; }
    }
}
