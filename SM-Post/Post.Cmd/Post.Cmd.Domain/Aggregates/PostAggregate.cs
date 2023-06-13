using CQRS.Core.Domain;
using Post.Common.Events;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostAggregate : AggregateRoot
    {
        private bool _active;
        private string _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();
        public bool Active => _active;

        public PostAggregate()
        {
        }

        public PostAggregate(Guid id, string author, string message)
        {
            RaiseEvent(new PostCreatedEvent
            {
                Id = id,
                Author = author,
                Message = message,
                PostCreatedDate = DateTime.Now
            });
        }

        public void Apply(PostCreatedEvent @event)
        {
            _id = @event.Id;
            _author = @event.Author;
            _active = true;
        }

        public void MessageUpdate(string message)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit an inactive post!");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException($"The value of {message} cannot be null or empty. Please provide a valid!");
            }

            RaiseEvent(new MessageUpdateEvent
            {
                Id = _id,
                Message = message
            });
        }

        public void Apply(MessageUpdateEvent @event)
        {
            _id = @event.Id;
        }

        public void PostLiked()
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot like an inactive post!");
            }

            RaiseEvent(new PostLikedEvent
            {
                Id = _id,
            });
        }

        public void Apply(PostLikedEvent @event)
        {
            _id = @event.Id;
        }

        public void CommentCreated(string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add a comment to an inactive post!");
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new InvalidOperationException($"The value of {comment} cannot be null or empty. Please provide a valid!");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidOperationException($"The value of {username} cannot be null or empty. Please provide a valid!");
            }

            RaiseEvent(new CommentCreatedEvent
            {
                Id = _id,
                Comment = comment,
                CommnetId = Guid.NewGuid(),
                Username = username,
                CommentAddedDate = DateTime.Now
            });
        }

        public void Apply(CommentCreatedEvent @event)
        {
            _id = @event.Id;
            _comments.Add(@event.CommnetId, new Tuple<string, string>(@event.Comment, @event.Username));
        }

        public void CommentUpdated(Guid commentId, string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot update a comment on an inactive post!");
            }

            if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You are not allowed to edit a comment that was made by another user");
            }

            RaiseEvent(new CommentUpdatedEvent
            {
                Id = _id,
                Comment = comment,
                CommnetId = commentId,
                Username = username,
                CommentUpdatedDate = DateTime.Now
            });
        }

        public void Apply(CommentUpdatedEvent @event)
        {
            _id = @event.Id;
            _comments[@event.CommnetId] = new Tuple<string, string>(@event.Comment, @event.Username);
        }

        public void CommentDelete(Guid commentId, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot delete a comment on an inactive post!");
            }

            if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You are not allowed to delete a comment that was made by another user");
            }

            RaiseEvent(new CommentDeleteEvent
            {
                Id = _id,
                CommnetId = commentId,
            });
        }

        public void Apply(CommentDeleteEvent @event)
        {
            _id = @event.Id;
            _comments.Remove(@event.CommnetId);
        }

        public void PostDelete(string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot delete an inactive post!");
            }

            if (!_author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException("You are not allowed to delete a post that was made by another user");
            }

            RaiseEvent(new PostDeleteEvent
            {
                Id = _id,
            });
        }

        public void Apply(PostDeleteEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }
    }
}
