using System;
using logintest.Areas.Identity.Data;
using logintest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(logintest.Areas.Identity.IdentityHostingStartup))]
namespace logintest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<logintestDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("logintestDbContextConnection")));

                services.AddDefaultIdentity<AccountUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<logintestDbContext>();
            });
        }
    }
}