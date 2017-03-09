namespace Devanooga.Jobs.Data
{
    using Entity.DevanoogaJobs;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{env.ContentRootPath}/../Devanooga.Jobs.Website")
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
            services
                .AddSingleton(this.Configuration)
                .AddSingleton(this.Environment);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var logger = loggerFactory.CreateLogger<Startup>();
        }
    }
}
