using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class CommentUpdatedEvent : BaseEvent
    {
        public CommentUpdatedEvent() : base(nameof(CommentUpdatedEvent))
        {
        }

        public Guid CommnetId { get; set; }
        public string? Comment { get; set; }
        public string? Username { get; set; }
        public DateTime CommentUpdatedDate { get; set; }
    }

}
