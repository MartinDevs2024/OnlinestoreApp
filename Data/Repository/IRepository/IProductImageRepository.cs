using EcommerceApp.Models;

namespace EcommerceApp.Data.Repository.IRepository
{
    public interface IProductImageRepository: IRepository<ProductImage>
    {
        void Update(ProductImage obj);
    }
}
