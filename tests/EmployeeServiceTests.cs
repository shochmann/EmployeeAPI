using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Services;

namespace EmployeeTests;

[TestClass]
public class EmployeeServiceTests
{
    private EmployeeData _employeeData;
    private EmployeeService _service;

    [TestInitialize]
    public void Setup()
    {
        _employeeData = new EmployeeData();
        _service = new EmployeeService(_employeeData);
    }

    [TestMethod]
    public async Task GetEmployeesAsync_ReturnsAllEmployees()
    {
        var result = await _service.GetEmployeesAsync();
        Assert.AreEqual(3, result.EmployeeList.Count());
    }

    [TestMethod]
    public async Task CreateEmployeeAsync_VerifyIdCreation()
    {
        var newEmployee = new Employee
        {
            FirstName = "test1",
            LastName = "last1",
            Email = "test@test.com"
        };

        var employees = await _service.GetEmployeesAsync();
        var maxId = employees.EmployeeList.Max(e => e.Id);
        var nextId = maxId + 1;
        var createdEmployee = await _service.CreateEmployeeAsync(newEmployee);

        Assert.AreEqual(createdEmployee.Id, nextId);
    }
}
