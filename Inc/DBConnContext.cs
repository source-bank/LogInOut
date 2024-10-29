using Microsoft.EntityFrameworkCore;

namespace LogInOut.Inc
{
    public class DBConnContext : DbContext
    {
        public DBConnContext(DbContextOptions<DBConnContext> options) : base(options) { }
    }
}
