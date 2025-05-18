using Api.Dtos.Dependent;

namespace Api.Services.Interfaces
{
    public interface IDependentService
    {
        Task<GetDependentDto?> GetDependentByIdAsync(int id);
        Task<List<GetDependentDto>> GetDependentsByEmployeeIdAsync(int id);
        Task<List<GetDependentDto>> GetAllDependentsAsync();
        Task<int> AddDependentAsync(PostDependentDto dependent);
        Task<bool> UpdateDependentAsync(PostDependentDto dependent);
        Task<bool> DeleteDependentAsync(int id);
    }
}
