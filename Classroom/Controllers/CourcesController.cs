using Classroom.Context;
using Classroom.Entities;
using Classroom.Filters;
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
[TypeFilter(typeof(IsCourceExistsActionFilterAttribute))]
[TypeFilter(typeof(IsTaskExistsActionFilterAttribute))]
public partial class CourcesController:ControllerBase
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

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetCourceById(Guid courseId)
    {
        var cource = await _context.Cources!.FirstOrDefaultAsync( c => c.Id == courseId);
        return Ok(cource?.ToDto());
    }

    [HttpPut("{courseId}")]
    public async Task<IActionResult> UpdateCource(Guid courseId , [FromBody] UpdateCourceDto updateCourceDto)
    {
        if(!ModelState.IsValid) return BadRequest("bir nima ");

        var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courseId);

        if(cource is null) return NotFound();

        var user = await _userManager.GetUserAsync(User);

        if(cource.Users!.Any(c => c.UserId == user.Id && c.IsAdmin) != true) return BadRequest();

        cource.Name = updateCourceDto.Name;

        await _context.SaveChangesAsync();

        return Ok(cource.ToDto());
    }

    [HttpDelete("{courseId}")]
    public async Task<IActionResult> DeleteCource(Guid courseId)
    {
        var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courseId);

        var user = await _userManager.GetUserAsync(User);

        if(cource?.Users!.Any(c => c.UserId == user.Id && c.IsAdmin) != true) return Forbid();

        _context.Cources?.Remove(cource);
   
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{courseId}/join")]
    public async Task<IActionResult> JoinCource(Guid courseId,[FromBody] JoinCourceDto joinCourceDto)
    {
        var cource =await  _context.Cources!.FirstOrDefaultAsync(c => c.Id == courseId);

        if(cource is null) 
                    return NotFound();
        var user = await _userManager.GetUserAsync(User);

        if(cource.Users!.Any( uc => uc.UserId == user.Id) == true) return BadRequest("Siz ushbu kursda borsiz");

        _context.UserCources!.Add(new UserCource()
        { 
            UserId = user.Id,
            CourceId = cource.Id,
            IsAdmin = false
        });

        await _context.SaveChangesAsync();

        return Ok();
    }
}