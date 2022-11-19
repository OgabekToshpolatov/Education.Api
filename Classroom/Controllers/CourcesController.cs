using Classroom.Context;
using Classroom.Entities;
using Classroom.Mappers;
using Classroom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        var cources = await  _context.Cources!.ToListAsync();
        List<CourceDto> courceDto = cources.Select( c => c.ToDto()).ToList(); 
        return Ok(courceDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCource(CreateCourceDto createCourceDto)
    {
        if(!ModelState.IsValid) return BadRequest();

        var user = await _userManager.GetUserAsync(User); 

        var cource = new Cource()
        {
            Name = createCourceDto.Name,
            Key = Guid.NewGuid().ToString("N"),
            Users = new List<UserCource>()
            {
                new UserCource()
                {
                    UserId = user.Id,
                    IsAdmin = true
                }
            }
        };

        await _context.Cources!.AddAsync(cource);
        await _context.SaveChangesAsync();

        cource = await _context.Cources.FirstOrDefaultAsync( c => c.Id == cource.Id);

        return Ok(cource?.ToDto());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourceById(Guid id)
    {
        var cource = await _context.Cources!.FirstOrDefaultAsync( c => c.Id == id);

        if(cource is null) return NotFound();

        return Ok(cource.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCource(Guid id , [FromBody] UpdateCourceDto updateCourceDto)
    {
        if(!await _context.Cources!.AnyAsync(c => c.Id == id)) return NotFound();

        if(!ModelState.IsValid) return BadRequest("bir nima ");

        var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == id);

        if(cource is null) return NotFound();

        var user = await _userManager.GetUserAsync(User);

        if(cource.Users!.Any(c => c.UserId == user.Id && c.IsAdmin) != true) return BadRequest();

        cource.Name = updateCourceDto.Name;

        await _context.SaveChangesAsync();

        return Ok(cource.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCource(Guid id)
    {
        var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == id);

        if(cource is null) return NotFound();

        var user = await _userManager.GetUserAsync(User);

        if(cource.Users!.Any(c => c.UserId == user.Id && c.IsAdmin) != true) return Forbid();

        _context.Cources?.Remove(cource);
   
        await _context.SaveChangesAsync();

        return Ok();
    }
}