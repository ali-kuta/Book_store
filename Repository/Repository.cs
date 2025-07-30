using firs_dot_net_project.Data;
using firs_dot_net_project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Linq.Expressions;


namespace firs_dot_net_project.Repository

{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
             this.dbSet= _db.Set<T>(); 
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
            
        }

        public T Get(Expression<Func<T, bool>> filter , string? Categoryproparty = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(Categoryproparty))
            {

                foreach (var property in Categoryproparty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? Categoryproparty=null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(Categoryproparty)) { 

                foreach (var property in Categoryproparty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            
            }
            return query.ToList();
        }
        

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
