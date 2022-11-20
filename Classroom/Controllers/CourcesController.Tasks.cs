using Classroom.Mappers;
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
    // user cource azosi ekanligini tekshirish kerak.
         var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courceId);

         if(cource is null ) return NotFound();

         var tasks =  cource.Tasks!.Select( c => c.Adapt<TaskDto>()).ToList();

          return Ok(tasks ?? new List<TaskDto>());
   }

   [HttpGet("{courceId}/tasks/{taskId}")]
   public async Task<IActionResult> GetTaskById(Guid courceId, Guid taskId)
   {
      // user cource azosi ekanligini teskshirish kerak.
      //cource bor  yoki yuqligini teskhirish kk 
      var task =await _context.Tasks!.FirstOrDefaultAsync( t => t.Id == taskId && t.CourseId ==courceId);
      
      if(task is null) return NotFound();

      return Ok(task.Adapt<TaskDto>());
   }

   [HttpPut("{courceId}/tasks/{taskId}")]
   public async Task<IActionResult> UpdateTask(Guid courceId, Guid taskId,[FromBody] UpdateTaskDto updateTaskDto)
   {
      // user cource azosi ekanligini teskshirish kerak.
      //cource bor  yoki yuqligini teskhirish kk 
      var task =await _context.Tasks!.FirstOrDefaultAsync( t => t.Id == taskId && t.CourseId ==courceId);
      
      if(task is null) return NotFound();

      task.SetValues(updateTaskDto);

      await _context.SaveChangesAsync();

      return Ok(task.Adapt<TaskDto>());
   }



   [HttpDelete("{courceId}/tasks/{taskId}")]
   public async Task<IActionResult> DeleteTask(Guid courceId, Guid taskId)
   {
      // user cource admini ekanligini teskshirish kerak.
      //cource bor  yoki yuqligini teskhirish kk 
      var task =await _context.Tasks!.FirstOrDefaultAsync( t => t.Id == taskId && t.CourseId ==courceId);
      
      if(task is null) return NotFound();

      _context.Tasks!.Remove(task);
      await _context.SaveChangesAsync();
      return Ok();
   }



}