namespace Devanooga.Jobs.Data.Entity.DevanoogaJobs.Models
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System;

    public class Role : IdentityRole<Guid>
    {
        public Role() : base()
        {

        }

        public Role(string roleName) : base(roleName)
        {

        }
    }
}
