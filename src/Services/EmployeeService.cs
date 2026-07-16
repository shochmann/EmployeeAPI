using EmployeeAPI.Data;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeData _employeeData;

        public EmployeeService(EmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        public async Task<EmployeeData> GetEmployeesAsync()
        {
            return _employeeData;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            int newId;
            if (_employeeData.EmployeeList.Count() > 0)
            {
                newId = _employeeData.EmployeeList.Max(e => e.Id) + 1;
            }
            else
            {
                newId = 1;
            }

            employee.Id = newId;
            _employeeData.EmployeeList.Add(employee);
            return employee;
        }

        public Task<bool> DeleteEmployeeByIdAsync(int id)
        {
            var employee = _employeeData.EmployeeList.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return Task.FromResult(false);

            _employeeData.EmployeeList.Remove(employee);

            return Task.FromResult(true);
        }
    }
}
