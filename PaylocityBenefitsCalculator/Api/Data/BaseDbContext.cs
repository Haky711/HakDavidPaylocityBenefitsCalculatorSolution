using Api.Models;

namespace Api.Data
{
    public abstract class BaseDbContext
    {
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Dependent> Dependents { get; set; }

        public abstract Task SaveChangesAsync();
    }
}
