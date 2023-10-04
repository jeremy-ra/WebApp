using Sprout.Exam.Business.Factory;
using Sprout.Exam.Business.FactoryProvider;
using Sprout.Exam.Business.Services.Interfaces;
using Sprout.Exam.DataAccess.Interfaces;
using Sprout.Exam.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeTypes = Sprout.Exam.Common.Enums.EmployeeType;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        
        public EmployeeService(IUnitOfWork unitOfWork, IRepository<Employee> employeeRepository)
            : base(unitOfWork)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Add new employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee> Add(Employee employee)
        {
            _employeeRepository.Add(employee);

            await _unitOfWork.SaveChangesAsync();

            return employee;
        }
                
        /// <summary>
        /// Delete employee record.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task Delete(Employee employee)
        {
            _employeeRepository.Delete(employee);

            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Gets employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetById(id);
        }

        /// <summary>
        /// Gets employee list.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetAll();
        }

        /// <summary>
        /// Update employee record.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Task Update(Employee employee)
        {
            var employeeData = _employeeRepository.GetById(employee.Id);

            Employee employeeUpdate = employeeData.Result;
            employeeUpdate.FullName = employee.FullName;
            employeeUpdate.Birthdate = employee.Birthdate;
            employeeUpdate.Tin = employee.Tin;
            employeeUpdate.TypeId = employee.TypeId;

            _employeeRepository.Update(employeeUpdate);

            return _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Calculates employee salary based on employee type.
        /// </summary>
        /// <param name="employeeType"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        public decimal CalculateSalary(EmployeeTypes employeeType, decimal absentDays, decimal workedDays)
        {
            IEmployeeFactory employeeFactory;
            
            decimal employeeSalary = 0;
            switch (employeeType)
            {
                case EmployeeTypes.Regular:
                    employeeFactory = new EmployeeRegularFactory();
                    employeeSalary = employeeFactory.CalculateSalary(absentDays);
                    break;
                case EmployeeTypes.Contractual:
                    employeeFactory = new EmployeeContractualFactory();
                    employeeSalary = employeeFactory.CalculateSalary(workedDays);
                    break;
                // Add other employee types here.
            };

            return employeeSalary;
        }        
    }
}
