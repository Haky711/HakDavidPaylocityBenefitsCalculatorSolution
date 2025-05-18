using Api.Dtos.Employee;

namespace Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<GetEmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<List<GetEmployeeDto>> GetEmployeesAllAsync();
        Task<int> AddEmployeeAsync(PostEmployeeDto employee);
        Task<bool> UpdateEmployeeAsync(PostEmployeeDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
