using System.Linq.Expressions;
using firs_dot_net_project.Data;
using firs_dot_net_project.Models;
using firs_dot_net_project.Repository.IRepository;

namespace firs_dot_net_project.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }



        public void Update(Category obj)
        {
            _db.categories.Update(obj);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}