using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            try
            {

                var employees = await _employeeRepository.GetAllEmployees();
                if (employees == null) 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while fetching the list of employees");
                }
                if (employees.Count == 0)
                {
                    return Ok("No employee found.");
                }
                else
                    return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);

                if (employee == null)
                    return NotFound("No Employee Found");
                else
                    return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }

            
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                var createdEmployee = await _employeeRepository.CreateEmployee(employee);
                return StatusCode(StatusCodes.Status201Created, "Employee created successfully!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }

        }

        [HttpPut()]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            
            try
            {
                var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
                if (updatedEmployee == null)
                    return NotFound("Employee to update not found");
                else
                    return Ok("Employee has been updated successfully");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }


            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployee(id);
            try
            {
                if (!result)
                    return NotFound("Employee Not Found");
                else 
                {
                    return Ok("Employee deleted successfully");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }

        }
    }


}
