using EmployeeAPI.Controllers;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeTests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeService> _mockService;
        private EmployeeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockService.Object);
        }

        [TestMethod]
        public async Task GetEmployees_WhenEmployeeExists_ReturnsOk()
        {
            var employeeData = new EmployeeData
            {
                EmployeeList = new List<Employee>
                {
                    new Employee
                    {
                        Id = 1,
                        FirstName = "test1",
                        LastName = "last1",
                        Email = "test1@test.com"
                    },
                    new Employee
                    {
                        Id = 2,
                        FirstName = "test2",
                        LastName = "last3",
                        Email = "test2@test.com"
                    }
                }
            };

            _mockService
                .Setup(x => x.GetEmployeesAsync())
                .ReturnsAsync(employeeData);

            var result = await _controller.GetEmployees();
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);

            var returnedEmployees = okResult.Value as EmployeeData;

            Assert.IsNotNull(returnedEmployees);
            Assert.AreEqual(2, returnedEmployees.EmployeeList.Count());
        }

        [TestMethod]
        public async Task GetEmployees_WhenNoEmployeeExist_ReturnsNotFound()
        {
            var employeeData = new EmployeeData
            {
                EmployeeList = new List<Employee>()
            };
            _mockService
                .Setup(x => x.GetEmployeesAsync())
                .ReturnsAsync(employeeData);

            var result = await _controller.GetEmployees();
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task CreateEmployee_WhenEmployeeIsValid_ReturnsCreated()
        {
            var employee = new Employee
            {
                Id = 1,
                FirstName = "test1",
                LastName = "last1",
                Email = "testing@test.com"
            };

            _mockService
                .Setup(x => x.CreateEmployeeAsync(employee))
                .ReturnsAsync(employee);

            var result = await _controller.CreateEmployee(employee);
            var createdResult = result.Result as CreatedAtActionResult;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(employee, createdResult.Value);
        }

        [TestMethod]
        public async Task CreateEmployee_WhenFieldsAreEmpty_ReturnsBadRequest()
        {
            var employee = new Employee
            {
                FirstName = "",
                LastName = "test",
                Email = "test@test.com"
            };

            _controller.ModelState.AddModelError(nameof(Employee.FirstName), "First name is required.");

            _mockService
                .Setup(x => x.CreateEmployeeAsync(employee))
                .ReturnsAsync(employee);

            var result = await _controller.CreateEmployee(employee);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteEmployee_WhenEmployeeExists_ReturnsNoContent()
        {
            _mockService
                .Setup(x => x.DeleteEmployeeByIdAsync(1))
                .ReturnsAsync(true);

            var result = await _controller.DeleteEmployee(1);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteEmployee_WhenEmployeeDoesNotExist_ReturnsNotFound()
        {
            _mockService
                .Setup(x => x.DeleteEmployeeByIdAsync(10))
                .ReturnsAsync(false);

            var result = await _controller.DeleteEmployee(10);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}
