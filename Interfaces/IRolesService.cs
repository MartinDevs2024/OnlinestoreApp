using EcommerceApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Interfaces
{
    public interface IRolesService
    {
        public Task<bool> AddUserToRoleAsync(AppUser user, string roleName);

        public Task<List<IdentityRole>> GetRolesAsync();

        public Task<IEnumerable<string>> GetUserRolesAsync(AppUser user);

        public Task<List<AppUser>> GetUsersInRoleAsync(string roleName, int companyId);

        public Task<bool> IsUserInRoleAsync(AppUser member, string roleName);

        public Task<bool> RemoveUserFromRoleAsync(AppUser user, string roleName);

        public Task<bool> RemoveUserFromRolesAsync(AppUser user, IEnumerable<string> roleNames);

        public Task<string> GetCurrentRoleAsync(AppUser? user);

    }
}
