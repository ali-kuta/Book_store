using firs_dot_net_project.Models;

namespace firs_dot_net_project.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);

    }
}
