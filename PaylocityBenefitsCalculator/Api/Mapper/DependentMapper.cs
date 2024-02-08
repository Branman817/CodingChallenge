using Api.Dtos.Dependent;
using Api.Models;

namespace Api.Mapper;

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

    public static Dependent GetDependentDtoToDependent(GetDependentDto dto)
    {
        var dependent = new Dependent
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Relationship = dto.Relationship,
        };

        return dependent;
    }
}
