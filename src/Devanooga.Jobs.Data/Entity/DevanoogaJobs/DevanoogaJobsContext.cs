namespace Devanooga.Jobs.Data.Entity.DevanoogaJobs
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Threading.Tasks;

    public class DevanoogaJobsContext : IdentityDbContext<User, Role, Guid>
    {
        protected DevanoogaJobsContext() : base()
        {
        }

        public DevanoogaJobsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async Task Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if(!await this.Users.AnyAsync())
            {
                var adminRole = await roleManager.FindByNameAsync("admin");
                if(adminRole == null)
                {
                    adminRole = new Role("admin");
                    await roleManager.CreateAsync(adminRole);
                }

                var adminUser = new User("admin@devanooga.com");
                adminUser.Email = "admin@devanooga.com";
                await userManager.CreateAsync(adminUser, "password1");
                await userManager.SetLockoutEnabledAsync(adminUser, false);
                await userManager.AddToRoleAsync(adminUser, "admin");
            }
        }
    }
}
