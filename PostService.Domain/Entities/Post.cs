using PostService.Domain.Exceptions;
using PostService.Domain.Primitives;

namespace PostService.Domain.Entities;

public class Post : BaseEntity<Post>
{
    public Post(string title, string content)
    {
        Title = title;
        Content = content;
    }

    private Post()
    {
    }
    
    public string Title
    {
        get => _title;
        private set
        {
            if (value.Length < Constants.MinPostTitleLength)
            {
                throw new DomainValidationException(ExceptionMessages.PostTitleTooShort, 
                    Constants.MinPostTitleLength);
            }

            if (value.Length > Constants.MaxPostTitleLength)
            {
                throw new DomainValidationException(ExceptionMessages.PostTitleTooLong,
                    Constants.MaxPostTitleLength);
            }
            
            _title = value;
        }
    }
    
    private string _title;

    public string Content
    {
        get => _content;
        private set
        {
            if (value.Length < Constants.MinPostContentLength)
            {
                throw new DomainValidationException(ExceptionMessages.PostContentTooShort, 
                    Constants.MinPostContentLength);
            }

            if (value.Length > Constants.MaxPostContentLength)
            {
                throw new DomainValidationException(ExceptionMessages.PostContentTooLong,
                    Constants.MaxPostContentLength);
            }
            
            _content = value;
        }
    }
    
    private string _content;

    public Guid AuthorId
    {
        get => _authorId;
        private set
        {
            if (value == Guid.Empty)
            {
                throw new DomainValidationException(ExceptionMessages.InvalidAuthorId);
            }
            
            _authorId = value;
        }
    }
    
    private Guid _authorId;

    public Post SetAuthorId(Guid authorId)
    {
        AuthorId = authorId;
        return this;
    }
}