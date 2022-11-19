using Classroom.Entities;
using Classroom.Models;
using Mapster;

namespace Classroom.Mappers;

public static class CourceMapper
{
    public static CourceDto ToDto(this Cource cource)
    {
        return new CourceDto
        {
            Id = cource.Id,
            Name = cource.Name,
            Key = cource.Key,
            Users = cource.Users?.Select( userCource => userCource.User?.Adapt<UserDto>()).ToList()
        };
    } 
}