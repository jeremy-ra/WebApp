namespace Sprout.Exam.Business.Factory
{
    public abstract class EmployeeFactory : IEmployeeFactory
    {
        public abstract decimal CalculateSalary(decimal days);        
    }
}
