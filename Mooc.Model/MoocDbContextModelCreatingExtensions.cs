using Mooc.Model.Entity.Course;
using Mooc.Model.Entity.CourseChapter;
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
            
            b.Property(c => c.CourseName)
                .IsRequired()
                .HasMaxLength(CourseEntityConsts.MaxCourseNameLength);
            
            b.Property(c => c.Description)
                .HasMaxLength(CourseEntityConsts.MaxDescriptionLength);
            
            b.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            b.Property(c => c.CreatedAt)
                .IsRequired();
            
            // Configure one-to-many relationship: one course has many chapters
            b.HasMany(c => c.Chapters)
                .WithOne(ch => ch.Course)
                .HasForeignKey(ch => ch.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
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
