using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=127.0.0.1; port=5432; user id = postgres; password = postgres; database=aspnetcore; pooling = true";
            // var connectionString = "Server=.\\SQLEXPRESS2019;Initial Catalog=dbapicourse; MultipleActiveResultSets=true;User ID=sa;Password=12345";
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            optionBuilder.UseNpgsql(connectionString);
            // optionBuilder.UseSqlServer(connectionString);
            return new MyContext(optionBuilder.Options);
        }
    }

}