namespace Devanooga.Jobs.Website
{
    using Data.Entity.DevanoogaJobs;
    using Data.Entity.DevanoogaJobs.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.local.json", optional: true);

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();
            this.Environment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<DevanoogaJobsContext>(options =>
                    options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DevanoogaJobsContext, Guid>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";
                options.User.RequireUniqueEmail = true;
            });

            services.AddMvc();
            services
                .AddSingleton(this.Configuration)
                .AddSingleton(this.Environment);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var logger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            using (var serviceScope = app.ApplicationServices
                                         .GetRequiredService<IServiceScopeFactory>()
                                         .CreateScope())
            {
                logger.LogInformation("Migrating database");
                var userManager = app.ApplicationServices.GetService<UserManager<User>>();
                var roleManager = app.ApplicationServices.GetService<RoleManager<Role>>();
                var context = serviceScope.ServiceProvider.GetService<DevanoogaJobsContext>();
                context.Database.Migrate();
                Task.Run(() => context.Seed(userManager, roleManager)).Wait();
            }

            app.UseStaticFiles();
            app.UseMvc();
            app.UseIdentity();
        }
    }
}
