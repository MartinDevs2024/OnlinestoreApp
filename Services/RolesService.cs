using EcommerceApp.Data;
using EcommerceApp.Interfaces;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RolesService(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> AddUserToRoleAsync(AppUser user, string roleName)
        {
            try {
                if (user != null && !string.IsNullOrEmpty(roleName))
                {
                    bool result = (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
                    return result;
                }
                return false;
            } 
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            try
            {
                List<IdentityRole> result;
                result = await _context.Roles.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(AppUser user)
        {
            try
            {
                if (user != null)
                {
                    IEnumerable<string> result = await _userManager.GetRolesAsync(user);
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }        
        }

        public Task<string> GetCurrentRoleAsync(AppUser? user)
        {
            throw new NotImplementedException();
        }
        public Task<List<AppUser>> GetUsersInRoleAsync(string roleName, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserInRoleAsync(AppUser member, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRoleAsync(AppUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserFromRolesAsync(AppUser user, IEnumerable<string> roleNames)
        {
            throw new NotImplementedException();
        }
    }
}
