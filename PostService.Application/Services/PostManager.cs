using AuthService.Application.Primitives;
using AutoMapper;
using PostService.Application.Dto;
using PostService.Application.Exceptions;
using PostService.Application.Interfaces.Repositories;
using PostService.Application.Interfaces.Services;
using PostService.Domain.Entities;

namespace PostService.Application.Services;

public class PostManager : IPostManager
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostManager(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _mapper.Map<List<PostDto>>(await _postRepository.GetAllAsync(cancellationToken));
    }

    public async Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(id, cancellationToken);

        if (post == null)
        {
            throw new PostNotFoundException(ApplicationExceptionMessages.PostNotFoundWithId(id));
        }

        return _mapper.Map<PostDto>(post);
    }

    public async Task CreateAsync(Guid userId, PostCreateUpdateDto postDto, CancellationToken cancellationToken = default)
    {
        var post = _mapper.Map<Post>(postDto);

        post.SetAuthorId(userId);

        await _postRepository.CreateAsync(post, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, PostCreateUpdateDto postDto, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(id, cancellationToken);

        if (post == null)
        {
            throw new PostNotFoundException(ApplicationExceptionMessages.PostNotFoundWithId(id));
        }

        post.UpdatePost(postDto.Title, postDto.Content);

        await _postRepository.UpdateAsync(post, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(id, cancellationToken);

        if (post == null)
        {
            throw new PostNotFoundException(ApplicationExceptionMessages.PostNotFoundWithId(id));
        }

        await _postRepository.DeleteAsync(post, cancellationToken);
    }
}