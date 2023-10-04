using Sprout.Exam.Business.Factory;
using System;

namespace Sprout.Exam.Business.FactoryProvider
{
    public class EmployeeRegularFactory : EmployeeFactory
    {
        private const double basicSalary = 20000;
        private const double taxRate = 0.12;
        private const decimal numberOfWorkingDays = 22;

        /// <summary>
        /// Calculate salary for regular employee.
        /// </summary>
        /// <param name="absentDays"></param>
        /// <returns></returns>
        public override decimal CalculateSalary(decimal absentDays)
        {
            var totalWorkingDays = Convert.ToDouble(numberOfWorkingDays - absentDays);
            decimal salary = (decimal)(basicSalary - (basicSalary / totalWorkingDays) - (basicSalary * taxRate));
            // Add 2 decimal places.
            return Math.Round(salary, 2);
        }
    }
}
