using AuthService.Application.Dto;
using AuthService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetAllUsers(cancellationToken));
    }

    [HttpGet]
    [Route("user/{id}")]
    public async Task<ActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetUserById(id, cancellationToken));
    }

    [HttpPost]
    [Route("user")]
    public async Task<ActionResult> CreateUser([FromBody] UserCreateDto user, CancellationToken cancellationToken)
    {
        await _userService.CreateUser(user, cancellationToken);
        return Created();
    }

    [HttpPut]
    [Route("user")]
    public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDto user, CancellationToken cancellationToken)
    {
        await _userService.UpdateUser(user, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("user/{id}")]
    public async Task<ActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        await _userService.DeleteUser(id, cancellationToken);
        return Ok();
    }
}