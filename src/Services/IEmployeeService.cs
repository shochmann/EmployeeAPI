using EmployeeAPI.Data;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeData> GetEmployeesAsync();
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeByIdAsync(int id);
    }
}
