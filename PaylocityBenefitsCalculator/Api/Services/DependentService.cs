using Api.Dtos.Dependent;
using Api.Exceptions;
using Api.Extensions;
using Api.Models;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class DependentService : BaseService, IDependentService
    {
        private readonly IDependentRepository repository;

        public DependentService(IDependentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> AddDependentAsync(PostDependentDto dependent)
        {
            this.CheckRelationshipConstraint(dependent);
            var result = await this.repository.AddAsync(dependent.ToModel());
            await this.repository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteDependentAsync(int id)
        {
            var result = await this.repository.DeleteAsync(id);
            this.IfFalseTrowNotFoundException(result, $"Dependent with id {id} not found!");
            await this.repository.SaveChangesAsync();
            return result;
        }

        public async Task<List<GetDependentDto>> GetAllDependentsAsync()
        {
            var result = await this.repository.GetAllAsync();
            return result.Select(x => x.ToDto()).ToList();
        }

        public async Task<GetDependentDto?> GetDependentByIdAsync(int id)
        {
            var result = await this.repository.GetByIdAsync(id);
            this.IfNullThrowNotFoundException(result, $"Dependent with id {id} not found!");
            return result!.ToDto();
        }

        public async Task<List<GetDependentDto>> GetDependentsByEmployeeIdAsync(int id)
        {
            var result = await this.repository.GetDependentsByEmployeeIdAsync(id);            
            return result.Select(x => x.ToDto()).ToList();
        }

        public async Task<bool> UpdateDependentAsync(PostDependentDto dependent)
        {
            this.CheckRelationshipConstraint(dependent);
            var result = await this.repository.UpdateAsync(dependent.ToModel());
            this.IfFalseTrowNotFoundException(result, $"Dependent with id {dependent.Id} not found!");
            await this.repository.SaveChangesAsync();
            return result;
        }

        private void CheckRelationshipConstraint(PostDependentDto dependent)
        {
            if (dependent.Relationship == Relationship.Spouse ||
                dependent.Relationship == Relationship.DomesticPartner)
            {
                var otherDependents = this.repository.GetDependentsByEmployeeIdAsync(dependent.EmployeeId);

                if (otherDependents.Result.Any(x =>
                    x.Relationship == Relationship.Spouse ||
                    x.Relationship == Relationship.DomesticPartner))
                {
                    throw new NotPossibleToInsertException("An employee can only have one spouse or domestic partner!");
                }
            }
        }
    }
}
