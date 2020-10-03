using Api.Data.Context;
using Api.Data.Repository;
using Api.Data.Implementations;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependencyRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();


            serviceCollection.AddDbContext<MyContext>(
                  options => options.UseNpgsql("Server=127.0.0.1; port=5432; user id = postgres; password = postgres; database=aspnetcore; pooling = true")
              );
        }

    }
}