namespace Devanooga.Jobs.Data.Entity.DevanoogaJobs.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;

    public class User : IdentityUser<Guid>
    {
        public User() : base()
        {

        }

        public User(string userName)
        {
            this.UserName = userName;
        }
    }
}
