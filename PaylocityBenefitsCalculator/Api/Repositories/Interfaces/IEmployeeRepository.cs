using Api.Models;

namespace Api.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<ICollection<Employee>> GetAllAsync();
        Task<int> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}
