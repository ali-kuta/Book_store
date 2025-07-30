using System.Linq.Expressions;

namespace firs_dot_net_project.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(string? Categoryproparty = null);
        
        T Get(Expression<Func<T, bool>> filter , string? Categoryproparty = null);
        void Add(T entity);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
