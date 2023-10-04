using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _appDbContext;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);            
        }

        /// <summary>
        /// Gets list of entity
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
