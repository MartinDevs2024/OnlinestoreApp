using EcommerceApp.Data.Repository.IRepository;
using EcommerceApp.Models;

namespace EcommerceApp.Data.Repository
{
    public class ApplicationUserRepository : Repository<AppUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AppUser applicationUser)
        {
            _db.ApplicationUsers.Update(applicationUser);
        }
    }
}
