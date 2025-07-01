using firs_dot_net_project.Models;

namespace firs_dot_net_project.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
       void Update(Category obj);
        void Save();



    }
}
