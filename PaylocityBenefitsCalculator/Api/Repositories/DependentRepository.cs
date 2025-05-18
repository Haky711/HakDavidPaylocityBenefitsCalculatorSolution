using Api.Data;
using Api.Models;
using Api.Repositories.Interfaces;
using System.Security.Cryptography;

namespace Api.Repositories
{
    public class DependentRepository : BaseRepository, IDependentRepository
    {
        // Maybe would make a sense to think about generic repository pattern
        public DependentRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<Dependent?> GetByIdAsync(int id)
        {
            return this.context.Dependents.FirstOrDefault(x => x.Id == id);
        }


        public async Task<ICollection<Dependent>> GetDependentsByEmployeeIdAsync(int id)
        {
            return this.context.Dependents.Where(x => x.EmployeeId == id).ToList();
        }

        public async Task<ICollection<Dependent>> GetAllAsync()
        {
            return this.context.Dependents.ToList();
        }

        public async Task<int> AddAsync(Dependent dependent)
        {
            int newId = this.context.Dependents.Any() ? this.context.Dependents.Max(p => p.Id) + 1 : 1;
            dependent.Id = newId;
            this.context.Dependents.Add(dependent);
            return newId;
        }

        public async Task<bool> UpdateAsync(Dependent dependent)
        {
            var existingDependent = this.context.Dependents.FirstOrDefault(x => x.Id == dependent.Id);

            if (existingDependent == null)
            {
                return false;
            }

            existingDependent.FirstName = dependent.FirstName;
            existingDependent.LastName = dependent.LastName;
            existingDependent.Relationship = dependent.Relationship;
            existingDependent.DateOfBirth = dependent.DateOfBirth;
            existingDependent.EmployeeId = dependent.EmployeeId;

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dependent = this.context.Dependents.FirstOrDefault(x => x.Id == id);

            if (dependent != null)
            {
                this.context.Dependents.Remove(dependent);
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
