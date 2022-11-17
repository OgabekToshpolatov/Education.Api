using Classroom.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Classroom.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController:ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager ;
        _signInManager = signInManager ;
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp()
    {
        return Ok();
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn()
    {
        return Ok();
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> Profile()
    {
        return Ok();
    }
}