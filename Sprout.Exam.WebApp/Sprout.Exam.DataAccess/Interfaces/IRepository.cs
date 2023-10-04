using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
