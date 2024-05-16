using EcommerceApp.Models;

namespace EcommerceApp.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
