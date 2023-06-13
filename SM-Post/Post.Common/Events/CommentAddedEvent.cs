using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class CommentAddedEvent : BaseEvent
    {
        public CommentAddedEvent() : base(nameof(CommentAddedEvent))
        {
        }

        public Guid CommnetId { get; set; }
        public string? Comment { get; set; }
        public string? Username { get; set; }
        public DateTime CommentAddedDate { get; set; }
    }
}
