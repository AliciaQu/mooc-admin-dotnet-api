using Mooc.Model.Entity.Course;
using Mooc.Model.Entity.CourseChapter;
using Mooc.Model.Entity.Category;

using Microsoft.EntityFrameworkCore;
using Mooc.Model.Entity;  // make sure your entities are in this namespace

namespace Mooc.Model.DBContext
{
    public class MoocDBContext : DbContext
    {
        public MoocDBContext(DbContextOptions<MoocDBContext> options) : base(options)
        {
        }

        #region demo
        public DbSet<Test> Tests { get; set; }

        #endregion

        #region DbSets

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseChapter> CourseChapters { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureDemoManagement();
            modelBuilder.ConfigureUserManagement();
            modelBuilder.ConfigureCategoryManagement();
            modelBuilder.ConfigureCourseManagement();

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }
}

