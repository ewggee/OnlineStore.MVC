using Microsoft.AspNetCore.Identity;
using OnlineStore.Contracts.ApplicationUsers;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Users.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> UpdateUserAsync(AppUserDto appUserDto)
        {
            var user = await _userManager.FindByIdAsync(appUserDto.Id.ToString());

            user!.UserName = appUserDto.UserName;
            user.Email = appUserDto.Email;
            user.PhoneNumber = appUserDto.PhoneNumber;

            return await _userManager.UpdateAsync(user);
        }
    }
}
