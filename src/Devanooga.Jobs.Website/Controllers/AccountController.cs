namespace Devanooga.Jobs.Website.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class AccountController
    {
        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.SignInManager = signInManager;
            this.UserManager = userManager;
        }

        protected SignInManager<IdentityUser> SignInManager { get; }

        protected UserManager<IdentityUser> UserManager { get; }
    }
}
