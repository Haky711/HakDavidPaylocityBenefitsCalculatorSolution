using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class EmployeeFacadeService : BaseService, IEmployeeFacadeService
    {
        private readonly IEmployeeService employeeService;
        private readonly IDependentService dependentService;

        public EmployeeFacadeService(IEmployeeService employeeService, IDependentService dependentService)
        {
            this.employeeService = employeeService;
            this.dependentService = dependentService;
        }

        public async Task<int> AddEmployeeWithDependentsAsync(PostEmployeeDto employee)
        {   
            var returnId = await employeeService.AddEmployeeAsync(employee);

            foreach (var dependent in employee.Dependents)
            {
                dependent.EmployeeId = returnId; // Set the EmployeeId for each dependent
                await dependentService.AddDependentAsync(dependent); // Method for adding a collection should be added
            }

            return returnId;
        }

        public async Task<bool> DeleteEmployeeWithDependentsAsync(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            var employeeDependents = await dependentService.GetDependentsByEmployeeIdAsync(employee.Id);

            foreach (var dependent in employee.Dependents)
            {
                await dependentService.DeleteDependentAsync(dependent.Id); // Method for deleting a collection should be added
            }

            return await employeeService.DeleteEmployeeAsync(id);
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployeesWithDependentsAsync()
        {
            var employees = await employeeService.GetEmployeesAllAsync();

            foreach (var employee in employees)
            {
                var dependents = await dependentService.GetDependentsByEmployeeIdAsync(employee.Id);
                employee.Dependents = dependents;
            }

            return employees;
        }

        public async Task<GetEmployeeDto?> GetEmployeeWithDependentsByIdAsync(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            var dependents = await dependentService.GetDependentsByEmployeeIdAsync(employee.Id);
            employee.Dependents = dependents;
            return employee;
        }

        public async Task<bool> UpdateEmployeeWithDependentsAsync(PostEmployeeDto employee)
        {
            foreach (var dependent in employee.Dependents)
            {
                dependent.EmployeeId = employee.Id; // Set the EmployeeId for each dependent
                await dependentService.UpdateDependentAsync(dependent); // Method for updating a collection should be added
            }

            return await employeeService.UpdateEmployeeAsync(employee);
        }

        // Would make sense to add possibility to switch between weekly, bi-weekly and monthly paycheck
        public async Task<GetPaycheckDto?> GetPaycheckByEmployeeIdAsync(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            var dependents = await dependentService.GetDependentsByEmployeeIdAsync(employee.Id);
            var ageLimit = 50;

            var paycheck = new GetPaycheckDto
            {
                Employee = employee,
                GrossPay = employee.Salary / 26,
                Expences = new Dictionary<string, decimal>()
            };

            paycheck.Expences.Add("Base benefits", ConvertMonthExpenseToTwoWeeks(1000));

            foreach (var dependent in dependents)
            {
                paycheck.Expences.Add($"Additional benefit for '{dependent.FirstName} {dependent.LastName}'", ConvertMonthExpenseToTwoWeeks(500));

                if (dependent.DateOfBirth < DateTime.Now.AddYears(-ageLimit))
                {
                    paycheck.Expences.Add($"Additional benefit for '{dependent.FirstName} {dependent.LastName}' (over {ageLimit})", ConvertMonthExpenseToTwoWeeks(200));
                }
            }

            if (employee.Salary > 80000)
            {
                paycheck.Expences.Add("Income benefit", ConvertMonthExpenseToTwoWeeks(paycheck.GrossPay * 0.02M / 12));
            }            

            paycheck.Payroll = paycheck.GrossPay - paycheck.Expences.Values.Sum();

            return paycheck;
        }

        private decimal ConvertMonthExpenseToTwoWeeks(decimal monthExpense)
        {
            return (monthExpense / (362 / 12)) * 14; // Average month has 30.42 days, so 14 days is 14/30.42 = 0.4592 months
        }
    }
}
