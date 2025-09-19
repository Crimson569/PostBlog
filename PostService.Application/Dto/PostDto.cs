namespace PostService.Application.Dto;

public record PostDto(Guid Id, string Title, string Content, string AuthorId);