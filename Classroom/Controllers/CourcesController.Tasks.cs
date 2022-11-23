using Classroom.Mappers;
using Classroom.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Controllers;

public partial class CourcesController
{
   [HttpPost("{courseId}/tasks")]
   public async Task<IActionResult> AddTask(Guid courseId, [FromBody] CreateTaskDto createTaskDto)
   {
       if(!ModelState.IsValid) return BadRequest();

       var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courseId);

       if(cource is null) return NotFound();

       var user = await _userManager.GetUserAsync(User);

       if(cource.Users!.Any( uc => uc.UserId == user.Id && uc.IsAdmin) != true) return Forbid();

       var task = createTaskDto.Adapt<Classroom.Entities.Task>();

       task.CreatedDate = DateTime.Now;
       task.CourseId = courseId;

       await _context.Tasks!.AddAsync(task);
       await _context.SaveChangesAsync();

       return Ok(task.Adapt<TaskDto>());
   }

   [HttpGet("{courseId}/tasks")]
   public async Task<IActionResult> GetTasks(Guid courseId)
   {
    // user cource azosi ekanligini tekshirish kerak.
         var cource = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courseId);

         if(cource is null ) return NotFound();

         var tasks =  cource.Tasks!.Select( c => c.Adapt<TaskDto>()).ToList();

          return Ok(tasks ?? new List<TaskDto>());
   }

   [HttpGet("{courseId}/tasks/{taskId}")]
   public async Task<IActionResult> GetTaskById(Guid courseId, Guid taskId)
   {
      // user cource azosi ekanligini teskshirish kerak.
      //cource bor  yoki yuqligini teskhirish kk 
      var task =await _context.Tasks!.FirstOrDefaultAsync( t => t.Id == taskId && t.CourseId ==courseId);
      
      if(task is null) return NotFound();

      return Ok(task.Adapt<TaskDto>());
   }

   [HttpPut("{courseId}/tasks/{taskId}")]
   public async Task<IActionResult> UpdateTask(Guid courseId, Guid taskId,[FromBody] UpdateTaskDto updateTaskDto)
   {
      // user cource azosi ekanligini teskshirish kerak.
      //cource bor  yoki yuqligini teskhirish kk 
      var task =await _context.Tasks!.FirstOrDefaultAsync( t => t.Id == taskId && t.CourseId ==courseId);
      
      if(task is null) return NotFound();

      task.SetValues(updateTaskDto);

      await _context.SaveChangesAsync();

      return Ok(task.Adapt<TaskDto>());
   }



   [HttpDelete("{courseId}/tasks/{taskId}")]
   public async Task<IActionResult> DeleteTask(Guid courseId, Guid taskId)
   {
      // user cource admini ekanligini teskshirish kerak.
      //cource bor  yoki yuqligini teskhirish kk 
      var task =await _context.Tasks!.FirstOrDefaultAsync( t => t.Id == taskId && t.CourseId ==courseId);
      
      if(task is null) return NotFound();

      _context.Tasks!.Remove(task);
      await _context.SaveChangesAsync();
      return Ok();
   }

   [HttpGet("{courseId}/tasks/{taskId}/results")]
   public async Task<IActionResult> GetTaskResults(Guid courseId, Guid taskId)
   {
      var task =await  _context.Tasks!.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
      if(task is null) 
               return NotFound();

      var taskDto = task.Adapt<UsersTaskResultsDto>();

      if(task.UserTasks is null) return Ok(taskDto);

      foreach(var result in task.UserTasks)
      {
         taskDto.UsersResult ??= new List<UsersTaskResult>();
         taskDto.UsersResult.Add(result.Adapt<UsersTaskResult>());
      }

      return Ok(taskDto);
      
   }

   [HttpPut("{courseId}/tasks/{taskId}/results/{resultId}")]
   public async Task<IActionResult> UpdateUserResult(Guid courseId, Guid taskId, Guid resultId, CreateUserTaskResultDto resultDto)
   {
       var task =await  _context.Tasks!.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
      if(task is null) 
               return NotFound();

       var result = task.UserTasks?.FirstOrDefault(usertask => usertask.Id == resultId);
       if(result is null) return NotFound();

       result.Status = resultDto.Status;
       result.Description = resultDto.Description;

       await _context.SaveChangesAsync();

       return Ok(result.Adapt<UserTaskResult>());        
   }



}