using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Implemtation.DataAccess
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<FlatDbContext>
    {
        public FlatDbContext CreateDbContext(string[] args)
        {
            var optBuilder = new DbContextOptionsBuilder<FlatDbContext>();
            optBuilder.UseSqlServer("Server=DESKTOP-1O8U0H5\\SQLEXPRESS;Database=FlatBot;Trusted_Connection=True;");

            return new FlatDbContext(optBuilder.Options);
        }
    }
}
