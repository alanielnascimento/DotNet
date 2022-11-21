using Microsoft.EntityFrameworkCore;
using PII.DATA.Model;

namespace PII.DATA
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Despesa> Despesas { get; set; }
    }
}
