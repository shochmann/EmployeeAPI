using EmployeeAPI.Models;

namespace EmployeeAPI.Data
{
    public class EmployeeData
    {
        public List<Employee> EmployeeList { get; set; } = new()
        {
            new Employee
            {
                Id = 1,
                FirstName = "Prem",
                LastName = "Tiwari",
                Email = "chapradreams@gmail.com"
            },
            new Employee
            {
                Id = 2,
                FirstName = "Vikash",
                LastName = "Kumar",
                Email = "abd@gmail.com"
            },
            new Employee
            {
                Id = 3,
                FirstName = "Vikash",
                LastName = "Kumar",
                Email = "asdjf@gmail.com"
            }
        };
    }
}
