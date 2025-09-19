using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Dto;
using PostService.Application.Interfaces.Services;

namespace PostService.Api.Controllers;

[Authorize]
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
    public async Task<ActionResult> GetPostsAsync(int page = 1, int pageSize = 5, CancellationToken cancellationToken = default)
    {
        var posts = await _postManager.GetAllAsync(page, pageSize, cancellationToken);
        return Ok(posts);
    }

    [HttpGet]
    [Route("post/{id:guid}")]
    public async Task<ActionResult> GetPostByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var post = await _postManager.GetByIdAsync(id, cancellationToken);
        return Ok(post);
    }

    [HttpPost]
    [Route("posts")]
    public async Task<ActionResult> CreatePostAsync(PostCreateUpdateDto postDto, CancellationToken cancellationToken = default)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                     User.FindFirst("userId")?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized();
        }
        
        await _postManager.CreateAsync(userId, postDto, cancellationToken);
        return Created();
    }

    [HttpPut]
    [Route("post/{id:guid}")]
    public async Task<ActionResult> UpdatePostAsync(Guid id, PostCreateUpdateDto postDto, CancellationToken cancellationToken = default)
    {
        await _postManager.UpdateAsync(id, postDto, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("Post/{id:guid}")]
    public async Task<ActionResult> DeletePostAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _postManager.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}