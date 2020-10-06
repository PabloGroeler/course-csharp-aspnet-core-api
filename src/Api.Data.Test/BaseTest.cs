using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            
        }
    }

    public class DbTeste: IDisposable {
        private string dataBaseName = $"aspnetcore2_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider serviceProvider {get; private set;}

        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
                o.UseNpgsql($"Server=127.0.0.1; port=5432; user id = postgres; password = postgres; database={dataBaseName}; pooling = true"),
                    ServiceLifetime.Transient
            );

            serviceProvider = serviceCollection.BuildServiceProvider();
            using(var context = serviceProvider.GetService<MyContext>()) {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose() {            
            using(var context = serviceProvider.GetService<MyContext>()) {
                context.Database.EnsureDeleted();
            }
        }
    }
    
}
