using System.Security.Claims;
using Classroom.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Classroom.Filters;

public class CourseAdminFilterAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public bool OnlyAdmin { get; set; }

    private readonly ILogger<CourseAdminFilterAttribute> _logger;

    public CourseAdminFilterAttribute(AppDbContext context, bool onlyAdmin, ILogger<CourseAdminFilterAttribute> logger)
    {
        _context = context ;
        OnlyAdmin = onlyAdmin ;
        _logger = logger ;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(!context.ActionArguments.ContainsKey("courseId"))
        {   _logger.LogInformation("ssdasdas");
            await next();
            return ;
        }

        var userClaims = context.HttpContext.User;
        var userId = userClaims.FindFirst(userClaim => userClaim.Type == ClaimTypes.NameIdentifier)?.Value;
        _logger.LogInformation("adfghfdsdfgdsa");
        var courseId = (Guid?)context.ActionArguments["courseId"];
        _logger.LogInformation("sdfghjkhgfdsaASDFGHJKFGHJKDFSA.");
        var cource = _context.Cources!.FirstOrDefault( c => c.Id == courseId);
        if(cource is null)
        {
            _logger.LogInformation("Mana bu yerda uxladi.");
            context.Result = new NotFoundResult();
            
            return ;
        }

        var userCource = cource?.Users?.FirstOrDefault(userCource => userCource.UserId.ToString() == userId);

        if(userCource is null)
        {
            _logger.LogInformation("Shotta uxladi");
              context.Result = new NotFoundResult();
              return ;
        }

        if(OnlyAdmin)
        {
            if(!userCource.IsAdmin)
            {
                _logger.LogInformation("admin emasligi uchun");
                context.Result = new BadRequestResult();
                return ;
            }
        }

        await next();


    }
}