using EcommerceApp.Models;

namespace EcommerceApp.Data.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
