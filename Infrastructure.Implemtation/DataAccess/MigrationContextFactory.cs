using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Implemtation.DataAccess
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<FlatDbContext>
    {
        public FlatDbContext CreateDbContext(string[] args)
        {
            var optBuilder = new DbContextOptionsBuilder<FlatDbContext>();
            optBuilder.UseSqlServer("Server=db-server;Database=FlatBot;User=sa;Password=4kMQcwxq");

            return new FlatDbContext(optBuilder.Options);
        }
    }
}
