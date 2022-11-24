using System.Security.Claims;
using Classroom.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Classroom.Filters;

public class CourseAdminFilterAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public bool OnlyAdmin { get; set; }
    public CourseAdminFilterAttribute(AppDbContext context, bool onlyAdmin)
    {
        _context = context ;
        OnlyAdmin = onlyAdmin ;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(!context.ActionArguments.ContainsKey("courseId"))
        {
            await next();
            return ;
        }

        var userClaims = context.HttpContext.User;
        var userId = userClaims.FindFirst(userClaim => userClaim.Type == ClaimTypes.NameIdentifier)?.Value;

        var courseId = (Guid?)context.ActionArguments["courseId"];

        var cource = _context.Cources!.FirstOrDefault( c => c.Id == courseId);
        if(cource is null)
        {
            context.Result = new NotFoundResult();
            return ;
        }

        var userCource = cource?.Users?.FirstOrDefault(userCource => userCource.Id.ToString() == userId);

        if(userCource is null)
        {
              context.Result = new NotFoundResult();
              return ;
        }

        if(OnlyAdmin)
        {
            if(!userCource.IsAdmin)
            {
                context.Result = new BadRequestResult();
                return ;
            }
        }

        await next();


    }
}