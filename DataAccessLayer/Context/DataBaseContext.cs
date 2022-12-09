using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace DataAccessLayer.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Definition> Definition { get; set; }
        public DbSet<LabratoaryTool> LabratoaryTool { get; set; }
        public DbSet<Piece> Piece { get; set; }
        public DbSet<PieceUsage> PieceUsage { get; set; }
        public DbSet<Process> Process { get; set; }
        public DbSet<Test> Test { get; set; }
    }
}
