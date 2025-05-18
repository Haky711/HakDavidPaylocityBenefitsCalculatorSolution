using Api.Models;

namespace Api.Dtos.Dependent
{
    public class PostDependentDto
    {
        public int Id { get; set; } // Keeping Id for update operations
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
        public int EmployeeId { get; set; }
    }
}
