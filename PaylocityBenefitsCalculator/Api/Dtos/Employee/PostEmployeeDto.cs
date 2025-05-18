using Api.Dtos.Dependent;

namespace Api.Dtos.Employee
{
    public class PostEmployeeDto
    {
        public int Id { get; set; } // Keeping Id for update operations
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<PostDependentDto> Dependents { get; set; } = new List<PostDependentDto>();
    }
}
