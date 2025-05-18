using Api.Dtos.Employee;
using Api.Dtos.Paycheck;

namespace Api.Services.Interfaces
{
    public interface IEmployeeFacadeService
    {
        Task<int> AddEmployeeWithDependentsAsync(PostEmployeeDto employee);
        Task<bool> UpdateEmployeeWithDependentsAsync(PostEmployeeDto employee);
        Task<bool> DeleteEmployeeWithDependentsAsync(int id);
        Task<GetEmployeeDto?> GetEmployeeWithDependentsByIdAsync(int id);
        Task<List<GetEmployeeDto>> GetAllEmployeesWithDependentsAsync();
        Task<GetPaycheckDto?> GetPaycheckByEmployeeIdAsync(int id);
    }
}
