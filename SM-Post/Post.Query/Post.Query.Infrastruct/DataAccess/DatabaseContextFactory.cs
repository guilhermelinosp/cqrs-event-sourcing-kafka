using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infrastruct.DataAccess
{
    public class DatabaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> optionsAction)
        {
            _optionsAction = optionsAction;
        }

        public DatabaseContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            _optionsAction(optionsBuilder);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
