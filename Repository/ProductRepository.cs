using firs_dot_net_project.Data;
using firs_dot_net_project.Models;
using firs_dot_net_project.Repository.IRepository;

namespace firs_dot_net_project.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository( ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
