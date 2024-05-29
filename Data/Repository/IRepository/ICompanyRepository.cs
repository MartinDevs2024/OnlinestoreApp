using EcommerceApp.Models;

namespace EcommerceApp.Data.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}
