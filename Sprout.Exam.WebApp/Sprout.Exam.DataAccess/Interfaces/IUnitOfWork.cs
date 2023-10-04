using System;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
