using PostService.Application.Dto;

namespace PostService.Application.Interfaces.Services;

public interface IPostService
{
    Task<IList<PostDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PostDto> GetAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(PostCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(PostCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}