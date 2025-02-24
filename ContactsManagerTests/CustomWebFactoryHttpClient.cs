
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ContactsManager.Infrastructure.MyDbContext;
using ContactManager2;
using System.Linq;

namespace ServiceCountryPersonTests
{
    public class CustomWebFactoryHttpClient: WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if(descriptor != null)  services.Remove(descriptor);
                services.AddDbContext<ApplicationDbContext>(options =>options.UseInMemoryDatabase("DataBaseTest"));
            });
        }
    }
}
