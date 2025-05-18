using Api.Dtos.Employee;
using Api.Extensions;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> AddEmployeeAsync(PostEmployeeDto employee)
        {
            var result = await this.repository.AddAsync(employee.ToModel());
            await this.repository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var result = await this.repository.DeleteAsync(id);
            this.IfFalseTrowNotFoundException(result, $"Employee with id {id} not found!");
            await this.repository.SaveChangesAsync();
            return result;
        }

        public async Task<List<GetEmployeeDto>> GetEmployeesAllAsync()
        {
            var result = await this.repository.GetAllAsync();
            return result.Select(x => x.ToDto()).ToList();
        }

        public async Task<GetEmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var result = await this.repository.GetByIdAsync(id);
            this.IfNullThrowNotFoundException(result, $"Employee with id {id} not found!");
            return result.ToDto();
        }

        public async Task<bool> UpdateEmployeeAsync(PostEmployeeDto employee)
        {
            var result = await this.repository.UpdateAsync(employee.ToModel());
            this.IfFalseTrowNotFoundException(result, $"Employee with id {employee.Id} not found!");
            await this.repository.SaveChangesAsync();
            return result;
        }
    }
}
