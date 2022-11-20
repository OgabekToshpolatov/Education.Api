using Classroom.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Controllers;

public partial class CourcesController
{
   [HttpPost("{courceId}/tasks")]
   public async Task<IActionResult> AddTask(Guid courceId, [FromBody] CreateTaskDto createTaskDto)
   {
       if(!ModelState.IsValid) return BadRequest();

       var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courceId);

       if(cource is null) return NotFound();

       var user = await _userManager.GetUserAsync(User);

       if(cource.Users!.Any( uc => uc.UserId == user.Id && uc.IsAdmin) != true) return Forbid();

       var task = createTaskDto.Adapt<Classroom.Entities.Task>();

       task.CreatedDate = DateTime.Now;
       task.CourseId = courceId;

       await _context.Tasks!.AddAsync(task);
       await _context.SaveChangesAsync();

       return Ok(task.Adapt<TaskDto>());
   }

   [HttpGet("{courceId}/tasks")]
   public async Task<IActionResult> GetTasks(Guid courceId)
   {
         var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courceId);

         if(cource is null ) return NotFound();

         var tasks =  cource.Tasks!.Select( c => c.Adapt<TaskDto>()).ToList();

          return Ok(tasks ?? new List<TaskDto>());
   }
}