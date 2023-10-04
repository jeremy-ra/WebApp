using Sprout.Exam.Business.Factory;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.FactoryProvider
{
    /// <summary>
    /// Class that handles employee type provider.
    /// </summary>
    public class EmployeeFactoryProvider
    {
        public EmployeeFactoryProvider()
        {
        }

        /// <summary>
        /// Gets employee factory provider.
        /// </summary>
        /// <param name="employeeType">Employee type(Regular, Contractual, etc...)</param>
        /// <returns></returns>
        public EmployeeFactory GetEmployeeFactory(EmployeeType employeeType)
        {
            return employeeType switch
            {
                EmployeeType.Regular => new EmployeeRegularFactory(),
                EmployeeType.Contractual => new EmployeeContractualFactory(),
                _ => null,
            };
        }
    }
}
