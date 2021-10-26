using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Implemtation.DataAccess
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<FlatDbContext>
    {
        public FlatDbContext CreateDbContext(string[] args)
        {
            var optBuilder = new DbContextOptionsBuilder<FlatDbContext>();
            optBuilder.UseSqlServer("Server=DESKTOP-JDVB3O9\\SQLEXPRESS01;Database=FlatBot;Trusted_Connection=True;");

            return new FlatDbContext(optBuilder.Options);
        }
    }
}
