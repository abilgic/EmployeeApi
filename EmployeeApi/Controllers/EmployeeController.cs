using EmployeeApi.Models;
using EmployeeApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        // GET: api/Employee
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await employeeRepository.GetEmployees();
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Employee> Get(int id)
        {
            return await employeeRepository.GetEmployee(id);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Employee employee)
        {
            // Logic to create new Employee
            try
            {
                if (employee == null)
                    return BadRequest();

                await employeeRepository.AddEmployee(employee);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee");
            }
        }

        [HttpPost]
        [Route("CreateEmployees")]
        public async Task<ActionResult<EmployeeDto>> CreateEmployees(EmployeeDto employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                await employeeRepository.AddEmployees(employee.Employees);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }


        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Employee employee)
        {
            // Logic to update an Employee
            try
            {
                if (employee == null)
                    return BadRequest();

                await employeeRepository.UpdateEmployee(employee);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                await employeeRepository.DeleteEmployee(id);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }

        }

    }
}
