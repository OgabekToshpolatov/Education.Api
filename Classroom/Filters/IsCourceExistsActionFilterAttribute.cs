using Classroom.Context;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Classroom.Filters;

public class IsCourceExistsActionFilterAttribute:ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public IsCourceExistsActionFilterAttribute(AppDbContext context)
    {
        _context = context ;
    }
    // public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    // {
    //     if(context.ActionArguments.ContainsKey("courceId"))
    // }
}