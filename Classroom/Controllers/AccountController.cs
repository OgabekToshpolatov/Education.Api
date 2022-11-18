using Classroom.Entities;
using Classroom.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> SignUp(SignUpUserDto creatUserDto)
    {
        if(!ModelState.IsValid) return BadRequest();

        if(creatUserDto.Password != creatUserDto.ConfirmPassword ) return BadRequest();

        if(await _userManager.Users.AnyAsync(u => u.UserName == creatUserDto.UserName))
           return BadRequest();

        var user = creatUserDto.Adapt<User>();

        await _userManager.CreateAsync(user, creatUserDto.Password);

        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok();
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInUserDto signInUserDto)
    {
        if(!ModelState.IsValid) return BadRequest();

        if(!await _userManager.Users.AnyAsync(u => u.UserName == signInUserDto.UserName))
                return NotFound();

        var result = await _signInManager.PasswordSignInAsync(signInUserDto.UserName, signInUserDto.Password, isPersistent: true, false);

        if(!result.Succeeded) return BadRequest();        

        return Ok();
    }
    
    [HttpGet("{username}")]
    [Authorize]
    public async Task<IActionResult> Profile(string username)
    {
        var user = await _userManager.GetUserAsync(User);

        if(user.UserName != username) return NotFound();

        var userDto = user.Adapt<UserDto>();

        return Ok(userDto);
    }
}