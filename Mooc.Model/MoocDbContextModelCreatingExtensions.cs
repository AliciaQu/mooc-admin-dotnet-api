using Mooc.Model.Entity;
using Mooc.Model.Entity.Course;
using Mooc.Model.Entity.CourseChapter;
using Mooc.Model.Entity.Category;
using Mooc.Shared.Entity.Course;
using Mooc.Shared.Entity.CourseChapter;

public static class MoocDbContextModelCreatingExtensions
{
    private const string TablePrefix = "";
    //private const string Schema = "";


    /// <summary>
    /// Demo Configuration
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureDemoManagement(this ModelBuilder modelBuilder)
    {
        // Test entity
        modelBuilder.Entity<Test>(b =>
        {
            b.ToTable(TablePrefix + "Test");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            b.Property(cs => cs.Title).IsRequired().HasMaxLength(TestEntityConsts.MaxTitleLength);
        });
    }

    /// <summary>
    /// User Management Configuration (User, UserRole, Teacher)
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureUserManagement(this ModelBuilder modelBuilder)
    {
        // User entity
        modelBuilder.Entity<User>(b =>
        {
            b.ToTable(TablePrefix + "Users");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            b.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);
            
            b.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            // Add unique indexes
            b.HasIndex(u => u.UserName)
                .IsUnique();
            
            b.HasIndex(u => u.Email)
                .IsUnique();
            
            b.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
            
            b.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            
            b.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);
            
            b.Property(u => u.Phone)
                .HasMaxLength(20);
            
            b.Property(u => u.Address)
                .HasMaxLength(255);
            
            b.Property(u => u.Gender);
            
            b.Property(u => u.Dob);
            
            b.Property(u => u.Avatar)
                .HasMaxLength(500);
            
            b.Property(u => u.Bio)
                .HasMaxLength(1000);
            
            b.Property(u => u.CreatedAt)
                .IsRequired();
            
            b.Property(u => u.UpdatedAt);
            
            // Configure many-to-many relationship: user has many roles
            b.HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<UserRole>(
                    j => j.HasOne(ur => ur.Role).WithMany(),
                    j => j.HasOne(ur => ur.User).WithMany()
                );
            
            // Configure one-to-one relationship: user has one teacher profile
            b.HasOne(u => u.TeacherProfile)
                .WithOne(t => t.User)
                .HasForeignKey<Teacher>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configure one-to-many relationship: user has many courses as teacher
            b.HasMany(u => u.CoursesAsTeacher)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // UserRole entity
        modelBuilder.Entity<UserRole>(b =>
        {
            b.ToTable(TablePrefix + "UserRoles");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            b.Property(ur => ur.UserId)
                .IsRequired();
            
            b.Property(ur => ur.RoleId)
                .IsRequired();
            
            b.Property(ur => ur.CreatedAt)
                .IsRequired();
            
            // Configure composite unique constraint
            b.HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();
        });
        
        // Teacher entity
        modelBuilder.Entity<Teacher>(b =>
        {
            b.ToTable(TablePrefix + "Teachers");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            b.Property(t => t.UserId)
                .IsRequired();
            
            // Add unique index for UserId
            b.HasIndex(t => t.UserId)
                .IsUnique();
            
            b.Property(t => t.Expertise)
                .HasMaxLength(255);
            
            // Configure one-to-one relationship: teacher belongs to user
            b.HasOne(t => t.User)
                .WithOne(u => u.TeacherProfile)
                .HasForeignKey<Teacher>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    /// <summary>
    /// Category Configuration
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureCategoryManagement(this ModelBuilder modelBuilder)
    {
        // Category entity
        modelBuilder.Entity<Category>(b =>
        {
            b.ToTable(TablePrefix + "Categories");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            b.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            b.Property(c => c.Description)
                .HasMaxLength(255);
            
            b.Property(c => c.ParentId);
            
            b.Property(c => c.SortOrder)
                .IsRequired()
                .HasDefaultValue(0);
            
            b.Property(c => c.Active)
                .IsRequired()
                .HasDefaultValue(true);
            
            b.Property(c => c.CreatedAt)
                .IsRequired();
            
            b.Property(c => c.UpdatedAt);
            
            // Configure self-referencing relationship: category has many subcategories
            b.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure one-to-many relationship: category has many courses
            b.HasMany(c => c.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    /// <summary>
    /// Course Management Configuration (Course + CourseChapter)
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureCourseManagement(this ModelBuilder modelBuilder)
    {
        // Course entity
        modelBuilder.Entity<Course>(b =>
        {
            b.ToTable(TablePrefix + "Courses");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            b.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(255);
            
            b.Property(c => c.Description)
                .HasMaxLength(1000);
            
            b.Property(c => c.CoverImage)
                .HasMaxLength(500);
            
            b.Property(c => c.CategoryId)
                .IsRequired();
            
            b.Property(c => c.TeacherId)
                .IsRequired();
            
            b.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue(1);
            
            b.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            b.Property(c => c.CreatedAt)
                .IsRequired();
            
            b.Property(c => c.UpdatedAt);
            
            // Configure one-to-many relationship: one course has many chapters
            b.HasMany(c => c.Chapters)
                .WithOne(ch => ch.Course)
                .HasForeignKey(ch => ch.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configure many-to-one relationship: course belongs to category
            b.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure many-to-one relationship: course belongs to teacher (user)
            b.HasOne(c => c.Teacher)
                .WithMany(u => u.CoursesAsTeacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // CourseChapter entity
        modelBuilder.Entity<CourseChapter>(b =>
        {
            b.ToTable(TablePrefix + "CourseChapters");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            // Foreign key
            b.Property(ch => ch.CourseId)
                .IsRequired();
            
            // Chapter name
            b.Property(ch => ch.ChapterName)
                .IsRequired()
                .HasMaxLength(CourseChapterEntityConsts.MaxChapterNameLength);
            
            // Chapter description
            b.Property(ch => ch.Description)
                .HasMaxLength(CourseChapterEntityConsts.MaxDescriptionLength);
            
            // Chapter order
            b.Property(ch => ch.OrderIndex)
                .IsRequired();
            
            // Chapter duration
            b.Property(ch => ch.Duration)
                .IsRequired()
                .HasDefaultValue(0);
            
            // Is active
            b.Property(ch => ch.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            // Is free
            b.Property(ch => ch.IsFree)
                .IsRequired()
                .HasDefaultValue(false);
            
            // Video URL
            b.Property(ch => ch.VideoUrl)
                .HasMaxLength(CourseChapterEntityConsts.MaxVideoUrlLength);
            
            // Material URL
            b.Property(ch => ch.MaterialUrl)
                .HasMaxLength(CourseChapterEntityConsts.MaxMaterialUrlLength);
            
            // Created at
            b.Property(ch => ch.CreatedAt)
                .IsRequired();
            
            // Created by
            b.Property(ch => ch.CreatedBy)
                .IsRequired();
            
            // Create indexes
            b.HasIndex(ch => ch.CourseId)
                .HasDatabaseName("IX_CourseChapters_CourseId");
            
            b.HasIndex(ch => new { ch.CourseId, ch.OrderIndex })
                .HasDatabaseName("IX_CourseChapters_CourseId_OrderIndex");
        });
    }
}
