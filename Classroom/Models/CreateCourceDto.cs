using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Classroom.Models;

public class CreateCourceDto
{
    [Required(ErrorMessage = "Required")]

    public string? Name { get; set; }

    public CreateCourceDto()
    {
        var culture = CultureInfo.CurrentCulture;
    }
}