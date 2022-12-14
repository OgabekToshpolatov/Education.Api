using Classroom.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Filters;

public class IsCourceExistsActionFilterAttribute:ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public IsCourceExistsActionFilterAttribute(AppDbContext context)
    {
        _context = context ;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(!context.ActionArguments.ContainsKey("courseId"))
        {
            await next(); // bu delegat bolgani uchun keyingisini chaqirib ketadi.
            return ;
        }

        var courseId = (Guid?)context.ActionArguments["courseId"];

        if(!await _context.Cources!.AnyAsync(course => course.Id == courseId))
        {
            context.Result = new NotFoundResult();
            return ;
        }

        await next();
    }
}