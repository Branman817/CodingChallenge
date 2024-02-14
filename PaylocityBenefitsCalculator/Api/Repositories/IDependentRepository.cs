using Api.Dtos.Dependent;

namespace Api.Repositories;

public interface IDependentRepository
{
    public Task<IEnumerable<GetDependentDto>> GetDependentsAsync();
    public Task<GetDependentDto> GetDependentByIdAsync(int id);
}
