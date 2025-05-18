using Api.Models;

namespace Api.Repositories.Interfaces
{
    public interface IDependentRepository : IBaseRepository
    {
        Task<Dependent?> GetByIdAsync(int id);
        Task<ICollection<Dependent>> GetAllAsync();
        Task<ICollection<Dependent>> GetDependentsByEmployeeIdAsync(int id);
        Task<int> AddAsync(Dependent dependent);
        Task<bool> UpdateAsync(Dependent dependent);
        Task<bool> DeleteAsync(int id);
    }
}
