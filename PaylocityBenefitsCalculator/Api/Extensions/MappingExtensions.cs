using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Extensions
{
    public static class MappingExtensions
    {
        // Usually I would use a library like AutoMapper for this, but for simplicity, I am doing it manually here.

        public static GetDependentDto ToDto(this Dependent dependent)
        {
            if (dependent == null)
            {
                return null;
            }

            return new GetDependentDto
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                DateOfBirth = dependent.DateOfBirth
            };
        }

        public static Dependent ToModel(this PostDependentDto dependent)
        {
            if (dependent == null)
            {
                return null;
            }

            return new Dependent
            {
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                DateOfBirth = dependent.DateOfBirth,
                EmployeeId = dependent.EmployeeId
            };
        }

        public static GetEmployeeDto ToDto(this Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            return new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents?.Select(d => d.ToDto()).ToList()
            };
        }

        public static Employee ToModel(this PostEmployeeDto employee)
        {
            if (employee == null)
            {
                return null;
            }

            return new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents?.Select(d => d.ToModel()).ToList()
            };
        }
    }
}
