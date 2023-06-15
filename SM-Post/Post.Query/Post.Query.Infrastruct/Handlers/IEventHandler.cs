using Post.Common.Events;

namespace Post.Query.Infrastructure.Handlers
{
    public interface IEventHandler
    {
        Task On(PostCreatedEvent @event);
        Task On(MessageUpdatedEvent @event);
        Task On(CommentRemovedEvent @event);
        Task On(CommentUpdatedEvent @event);
        Task On(PostLikedEvent @event);
        Task On(CommentCreatedEvent @event);
        Task On(PostRemovedEvent @event);
    }
}
