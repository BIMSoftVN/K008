using iTask.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTask.EF
{
    public class iTaskDbContext : DbContext
    {
        public DbSet<clUser> Users { get; set; }
        public DbSet<clTask> Tasks { get; set; }

        public iTaskDbContext(DbContextOptions<iTaskDbContext> op):base(op)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<clTask>().HasOne(o => o.NguoiGiao).WithMany().HasForeignKey(o => o.NguoiGiaoId);
            modelBuilder.Entity<clTask>().HasOne(o => o.NguoiNhan).WithMany().HasForeignKey(o => o.NguoiNhanId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
