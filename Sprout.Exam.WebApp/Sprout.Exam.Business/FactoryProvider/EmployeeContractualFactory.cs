using Sprout.Exam.Business.Factory;
using System;

namespace Sprout.Exam.Business.FactoryProvider
{
    public class EmployeeContractualFactory : EmployeeFactory
    {
        private const double ratePerDay = 500;

        /// <summary>
        /// Calculate salary for contractual employee.
        /// </summary>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        public override decimal CalculateSalary(decimal workedDays)
        {
            decimal salary = (decimal)ratePerDay * workedDays;
            // Add 2 decimal places.
            return Math.Round(salary, 2);
        }
    }
}
