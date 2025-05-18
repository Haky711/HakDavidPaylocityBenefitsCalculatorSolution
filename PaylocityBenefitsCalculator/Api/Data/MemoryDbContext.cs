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

        public void SeedData()
        {
            this.Employees = new List<Employee>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10)
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17)
                }
            };

            this.Dependents = new List<Dependent>()
            {
                new()
                {
                    Id = 1,
                    FirstName = "Spouse",
                    LastName = "Morant",
                    Relationship = Relationship.Spouse,
                    DateOfBirth = new DateTime(1998, 3, 3),
                    EmployeeId = 2
                },
                new()
                {
                    Id = 2,
                    FirstName = "Child1",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2020, 6, 23),
                    EmployeeId = 2
                },
                new()
                {
                    Id = 3,
                    FirstName = "Child2",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2021, 5, 18),
                    EmployeeId = 2
                },
                new()
                {
                    Id = 4,
                    FirstName = "DP",
                    LastName = "Jordan",
                    Relationship = Relationship.DomesticPartner,
                    DateOfBirth = new DateTime(1974, 1, 2),
                    EmployeeId = 3
                }
            };
        }

        public override Task SaveChangesAsync()
        {
            // In-memory database does not require saving changes
            return Task.CompletedTask; // No operation            
        }
    }
}
