using Microsoft.EntityFrameworkCore;
using EscAdv.Models;

namespace EscAdv.Data
{
    public class EscAdvContext : DbContext
    {
        public EscAdvContext (DbContextOptions<EscAdvContext> options)
            : base(options)
        {
        }

        public DbSet<Process> Process { get; set; }
    }
}