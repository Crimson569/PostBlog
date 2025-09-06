using PostService.Application.Dto;
using PostService.Domain.Entities;

namespace PostService.Application.Interfaces.Services;

public interface IPostManager
{
    Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(Guid userId, PostCreateUpdateDto postDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, PostCreateUpdateDto postDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}