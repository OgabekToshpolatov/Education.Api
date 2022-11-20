using Classroom.Context;
using Classroom.Entities;
using Classroom.Mappers;
using Classroom.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController:ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public ProfileController(AppDbContext context, UserManager<User> userManager)
    {
        _context = context ;
        _userManager = userManager ;
    }

    [HttpGet("/courses")]
    public async Task<IActionResult> GetCourses()
    {
        var user =await  _userManager.GetUserAsync(User);
        var courcesDto = user.Cources!.Select( uc => uc.Cource?.ToDto()).ToList();
        return Ok(courcesDto);
    }

    [HttpGet("courses/{courseId}/tasks")]
    public async Task<IActionResult> GetUserTasks(Guid courseId)
    {
       var user = await _userManager.GetUserAsync(User);

        var course = await _context.Cources!.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course?.Tasks is null)
            return NotFound();

        var tasks = course.Tasks;
        var usertasks = new List<UserTaskResultDto>();

        foreach (var task in tasks)
        {
            var result = task.Adapt<UserTaskResultDto>();
            var userResultEntity = task.UserTasks?.FirstOrDefault(ut => ut.UserId == user.Id);

            result.UserResult = userResultEntity == null ? null : new UserTaskResult()
                {
                    Status = userResultEntity.Status,
                    Description = userResultEntity.Description
                };

            usertasks.Add(result);
        }

        return Ok(usertasks);

    }

    [HttpPost("courses/{courseId}/tasks/{taskId}")]
    public async Task<IActionResult> AddUserTaskResult(Guid courseId, Guid taskId, [FromBody] CreateUserTaskResultDto resultDto)
    {
        //todo user kursni azosi ekanligini tekshirish kerak

        var task = await _context.Tasks!.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
        if (task is null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);

        var userTaskResult = await _context.UserTasks!
            .FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.TaskId == taskId);

        if (userTaskResult is null)
        {
            userTaskResult = new UserTask()
            {
                UserId = user.Id,
                TaskId = taskId
            };

            await _context.UserTasks!.AddAsync(userTaskResult);
        }

        userTaskResult.Description = resultDto.Description;
        userTaskResult.Status = resultDto.Status;

        await _context.SaveChangesAsync();

        return Ok();
    }




}