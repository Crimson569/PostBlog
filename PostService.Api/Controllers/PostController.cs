using Microsoft.AspNetCore.Mvc;
using PostService.Application.Dto;
using PostService.Application.Interfaces.Services;

namespace PostService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostManager _postManager;

    public PostController(IPostManager postManager)
    {
        _postManager = postManager;
    }

    [HttpGet]
    [Route("posts")]
    public async Task<ActionResult> GetAllPostsAsync(CancellationToken cancellationToken)
    {
        var posts = await _postManager.GetAllAsync(cancellationToken);
        return Ok(posts);
    }

    [HttpGet]
    [Route("post/{id:guid}")]
    public async Task<ActionResult> GetPostByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var post = await _postManager.GetByIdAsync(id, cancellationToken);
        return Ok(post);
    }

    [HttpPost]
    [Route("posts")]
    public async Task<ActionResult> CreatePostAsync(PostCreateUpdateDto postDto, CancellationToken cancellationToken)
    {
        await _postManager.CreateAsync(postDto, cancellationToken);
        return Created();
    }

    [HttpPut]
    [Route("post/{id:guid}")]
    public async Task<ActionResult> UpdatePostAsync(Guid id, PostCreateUpdateDto postDto, CancellationToken cancellationToken)
    {
        await _postManager.UpdateAsync(id, postDto, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("Post/{id:guid}")]
    public async Task<ActionResult> DeletePostAsync(Guid id, CancellationToken cancellationToken)
    {
        await _postManager.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}