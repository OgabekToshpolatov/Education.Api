using Classroom.Context;
using Microsoft.AspNetCore.Mvc;

namespace Classroom.Filters;

public class IsCourseAdminOrUserAttribute : TypeFilterAttribute
{
    public IsCourseAdminOrUserAttribute(bool onlyAdmin = false) : base(typeof(CourseAdminFilterAttribute))
    {
        Arguments = new object[] {onlyAdmin};
    }
}
    