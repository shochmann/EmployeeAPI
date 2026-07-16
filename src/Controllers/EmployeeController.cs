using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("getEmployees")]
        [HttpGet()]
        public async Task<ActionResult<EmployeeData>> GetEmployees() 
        {
            try
            {
                var employeeList = await _employeeService.GetEmployeesAsync();
                if (!employeeList.EmployeeList.Any())
                {
                    return NotFound(new
                    {
                        message = "No employees are found."
                    });
                }
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while retrieving employee data."
                });
            }
        }

        [Route("postEmployee")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest(new
                    {
                        message = "Employee data payload is incorrect."
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _employeeService.CreateEmployeeAsync(employee);

                return CreatedAtAction(
                    nameof(GetEmployees),
                    new { id = createdEmployee.Id },
                    createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while creating the employee."
                });
            }
        }

        [HttpDelete("deleteEmployeeById/{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeByIdAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = $"Employee with ID {id} was not found."
                });
            }

            return NoContent();
        }
    }
}
