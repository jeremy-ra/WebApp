using Sprout.Exam.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeTypes = Sprout.Exam.Common.Enums.EmployeeType;

namespace Sprout.Exam.Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> Add(Employee employee);
        public Task Update(Employee employee);
        public Task<Employee> GetEmployeeById(int id);
        public Task Delete(Employee employee);
        public decimal CalculateSalary(EmployeeTypes employeeType, decimal absentDays, decimal workedDays);
    }
}
