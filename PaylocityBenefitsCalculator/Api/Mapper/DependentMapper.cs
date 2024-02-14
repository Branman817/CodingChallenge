using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Mapper;

// Create static DependentMapper class to convert Dependent into GetDependentDto and Vice Versa
public static class DependentMapper
{
    public static GetDependentDto DependentToGetDependentDto(Dependent dependent)
    {
        var dto = new GetDependentDto
        {
            Id = dependent.Id,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship,
        };

        return dto;
    }
}
