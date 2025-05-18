using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using System.Linq;

namespace Api.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        // Maybe would make a sense to think about generic repository pattern
        public EmployeeRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return this.context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ICollection<Employee>> GetAllAsync()
        {
            return this.context.Employees.ToList();
        }

        public async Task<int> AddAsync(Employee employee)
        {
            int newId = this.context.Employees.Any() ? this.context.Employees.Max(p => p.Id) + 1 : 1;
            employee.Id = newId;
            this.context.Employees.Add(employee);
            return newId;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var existingEmployee = this.context.Employees.FirstOrDefault(x => x.Id == employee.Id);

            if (existingEmployee == null)
            {
                return false;
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.DateOfBirth = employee.DateOfBirth;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = this.context.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                this.context.Employees.Remove(employee);
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
