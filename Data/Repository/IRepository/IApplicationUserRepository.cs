using EcommerceApp.Models;

namespace EcommerceApp.Data.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<AppUser>
    {
        public void Update(AppUser applicationUser);
    }
}
