using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _appDbContext;

        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Saves or updates entity.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _appDbContext.Dispose();

        }
    }
}
