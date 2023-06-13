using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class CommentCreatedEvent : BaseEvent
    {
        public CommentCreatedEvent() : base(nameof(CommentCreatedEvent))
        {
        }

        public Guid CommnetId { get; set; }
        public string? Comment { get; set; }
        public string? Username { get; set; }
        public DateTime CommentAddedDate { get; set; }
    }
}
