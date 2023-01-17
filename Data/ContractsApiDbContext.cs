using contractsapi.Models;
using Microsoft.EntityFrameworkCore;

namespace contractsapi.Data
{
    public class ContractsApiDbContext: DbContext
    {
        public ContractsApiDbContext(DbContextOptions options):base(options)
        {

        }

        public  DbSet<Contracts> Contracts { get; set; }
    }
}
