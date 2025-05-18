using Api.Dtos.Dependent;
using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck
{
    public class GetPaycheckDto
    {
        public GetEmployeeDto Employee { get; set; }
        public decimal GrossPay { get; set; }
        public Dictionary<string, decimal> Expences { get; set; }
        public decimal Payroll { get; set; }
    }
}
