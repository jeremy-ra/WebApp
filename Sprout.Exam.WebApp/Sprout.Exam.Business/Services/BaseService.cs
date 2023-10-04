using Sprout.Exam.DataAccess.Interfaces;

namespace Sprout.Exam.Business.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
