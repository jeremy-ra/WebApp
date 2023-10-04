using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Sprout.Exam.Business.Services.Interfaces;
using Sprout.Exam.Entity.Entities;
using EmployeeTypes = Sprout.Exam.Common.Enums.EmployeeType;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        public EmployeesController(ILogger<EmployeesController> logger, IMapper mapper, IEmployeeService employeeService)
        {
            _logger = logger;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting list of employees.");

            var employees = await _employeeService.GetEmployees();

            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employees);            

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Getting employee by id {id}.");

            var employee = await _employeeService.GetEmployeeById(id);

            var data = _mapper.Map<EmployeeDto>(employee);

            if (data == null)
                return NotFound();
                        
            return Ok(data);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            _logger.LogInformation($"Updating employee with employee id {input.Id}.");

            var employee = await _employeeService.GetEmployeeById(input.Id);
            if (employee == null)
                return NotFound($"Employee id {input.Id} not found.");

            var data = _mapper.Map<Employee>(input);

            await _employeeService.Update(data);

            return Ok(input);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {     
            _logger.LogInformation("Creating new employee record.");

            var data = _mapper.Map<Employee>(input);

            var result = await _employeeService.Add(data);

            return Created($"/api/employees/{result.Id}", result.Id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting employee id  {id}.");

            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound($"Delete Employee id {id} not found.");

            await _employeeService.Delete(employee);

            return Ok(id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/{absentDays}/{workedDays}/calculate")]
        public async Task<IActionResult> Calculate(int id, decimal absentDays, decimal workedDays)
        {
            var result = await _employeeService.GetEmployeeById(id);

            if (result == null) return NotFound();
            var type = (EmployeeTypes)result.TypeId;

            _logger.LogInformation($"Calculating salary of employee id {id} with employee type {type}.");

            decimal salary = _employeeService.CalculateSalary(type, absentDays, workedDays);            

            return Ok(salary);
        }

    }
}
