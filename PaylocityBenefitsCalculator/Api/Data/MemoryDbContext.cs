using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Data
{
    public class MemoryDbContext : BaseDbContext
    {
        // Implementation of static fields to keep data also for scoped DbContext

        private static List<Employee> employees = new List<Employee>();
        public override ICollection<Employee> Employees
        {
            get => employees;
            set => employees = (List<Employee>)value;
        }


        private static List<Dependent> dependents = new List<Dependent>();
        public override ICollection<Dependent> Dependents
        {
            get => dependents;
            set => dependents = (List<Dependent>)value;
        }

        public MemoryDbContext()
        {
        }

        public override Task SaveChangesAsync()
        {
            // In-memory database does not require saving changes
            return Task.CompletedTask; // No operation            
        }
    }
}
