using DomainModel;
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
        public DbSet<Measure> Measure { get; set; }
        public DbSet<TestImportance> TestImportance { get; set; } 
        public DbSet<Standard> Standard { get; set; }
        public DbSet<ProcessType> ProcessType { get; set; }
        public DbSet<ControlPlan> ControlPlan { get; set; }
        public DbSet<TestCondition> TestCondition { get; set; }
        public DbSet<TestDescription> TestDescription { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ControlPlanPiece> ControlPlanPiece { get; set; }
        public DbSet<ControlPlanProcess> ControlPlanProcess { get; set; }
        public DbSet<ControlPlanProcessTest> ControlPlanProcessTest { get; set; }
        public DbSet<TestRequest> TestRequest { get; set; }
        public DbSet<TestRequestDetail> TestRequestDetail { get; set; }
        public DbSet<TestAccept> TestAccept { get; set; }
        public DbSet<TestAcceptDetail> TestAcceptDetail { get; set; }
        public DbSet<Systems> Systems { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<SystemMethod> SystemMethod { get; set; }
        public DbSet<SystemMethodUser> SystemMethodUser { get; set; }
        public DbSet<TestLabratoaryTool> TestLabratoaryTool { get; set; }
        public DbSet<RequestUser> RequestUser { get; set; }
        public DbSet<RequestUnit> RequestUnit { get; set; }
        public DbSet<UserAccess> UserAccess { get; set; }

    }
}
