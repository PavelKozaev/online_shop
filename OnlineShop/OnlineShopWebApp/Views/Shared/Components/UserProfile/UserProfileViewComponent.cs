using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.UserProfile
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;

        public UserProfileViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var avatarPath = user.Avatar;
            return View("UserProfile", avatarPath);
        }
    }
}
