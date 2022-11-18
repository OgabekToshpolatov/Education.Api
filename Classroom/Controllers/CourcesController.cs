using Classroom.Context;
using Classroom.Entities;
using Classroom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourcesController:ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public CourcesController(AppDbContext context, UserManager<User> userManager)
    {
        _context = context ;
        _userManager = userManager ;
    }

    [HttpGet]
    public async Task<IActionResult> GetCources( )
    {
        var users = await  _context.Cources!.ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCource(CreateCourceDto createCourceDto)
    {
        if(!ModelState.IsValid) return BadRequest();

        var user = await _userManager.GetUserAsync(User); 

        var cource = new Cource()
        {
            Name = createCourceDto.Name,
            Key = Guid.NewGuid().ToString("N")
        };


        await _context.Cources!.AddAsync(cource);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCource(Guid id)
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCource()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCource()
    {
        return Ok();
    }




}