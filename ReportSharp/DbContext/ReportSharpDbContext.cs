using Microsoft.EntityFrameworkCore;
using ReportSharp.Models;

namespace ReportSharp.DbContext
{
    public class ReportSharpDbContext : SoftDeletes.Core.DbContext
    {
        protected ReportSharpDbContext()
        {
        }

        public ReportSharpDbContext(DbContextOptions<ReportSharpDbContext> options) : base(options)
        {
        }

        public DbSet<ReportSharpRequest> ReportSharpRequests { get; set; }
        public DbSet<ReportSharpResponse> ReportSharpResponses { get; set; }
        public DbSet<ReportSharpData> ReportSharpDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReportSharpResponse>()
                .HasOne(x => x.ReportSharpRequest)
                .WithOne(x => x.ReportSharpResponse)
                .HasForeignKey<ReportSharpResponse>(x => x.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}