using Classroom.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Filters;

public class IsTaskExistsActionFilterAttribute:ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public IsTaskExistsActionFilterAttribute(AppDbContext context)
    {
        _context = context ;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(!context.ActionArguments.ContainsKey("taskId"))
        {
            await next();
            return ;
        }

        var taskId = (Guid?)context.ActionArguments["taskId"];

        if(!await _context.Tasks!.AnyAsync(task => task.Id == taskId))
        {
            context.Result = new NotFoundResult();
            return ;
        }

        await next();
    }
}