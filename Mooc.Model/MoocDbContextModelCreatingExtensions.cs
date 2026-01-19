using Mooc.Shared.Entity.CourseChapter;

public static class MoocDbContextModelCreatingExtensions
{
    private const string TablePrefix = "";
    //private const string Schema = "";


    /// <summary>
    /// Demo
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureDemoManagement(this ModelBuilder modelBuilder)
    {
        //Test
        modelBuilder.Entity<Test>(b =>
        {
            b.ToTable(TablePrefix + "Test");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            b.Property(cs => cs.Title).IsRequired().HasMaxLength(TestEntityConsts.MaxTitleLength);
        });
    }

    /// <summary>
    /// 课程章节管理配置
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureCourseChapterManagement(this ModelBuilder modelBuilder)
    {
        // CourseChapter（课程章节）
        modelBuilder.Entity<CourseChapter>(b =>
        {
            b.ToTable(TablePrefix + "CourseChapters");
            b.HasKey(x => x.Id);
            b.Property(e => e.Id).ValueGeneratedNever();
            
            // 外键（关联到课程，由Terence负责）
            b.Property(ch => ch.CourseId)
                .IsRequired();
            
            // 章节名称
            b.Property(ch => ch.ChapterName)
                .IsRequired()
                .HasMaxLength(CourseChapterEntityConsts.MaxChapterNameLength);
            
            // 章节描述
            b.Property(ch => ch.Description)
                .HasMaxLength(CourseChapterEntityConsts.MaxDescriptionLength);
            
            // 章节顺序
            b.Property(ch => ch.OrderIndex)
                .IsRequired();
            
            // 章节时长
            b.Property(ch => ch.Duration)
                .IsRequired()
                .HasDefaultValue(0);
            
            // 是否启用
            b.Property(ch => ch.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            // 是否免费
            b.Property(ch => ch.IsFree)
                .IsRequired()
                .HasDefaultValue(false);
            
            // 视频URL
            b.Property(ch => ch.VideoUrl)
                .HasMaxLength(CourseChapterEntityConsts.MaxVideoUrlLength);
            
            // 资料URL
            b.Property(ch => ch.MaterialUrl)
                .HasMaxLength(CourseChapterEntityConsts.MaxMaterialUrlLength);
            
            // 创建时间
            b.Property(ch => ch.CreatedAt)
                .IsRequired();
            
            // 创建人ID
            b.Property(ch => ch.CreatedBy)
                .IsRequired();
            
            // 创建索引
            b.HasIndex(ch => ch.CourseId)
                .HasDatabaseName("IX_CourseChapters_CourseId");
            
            b.HasIndex(ch => new { ch.CourseId, ch.OrderIndex })
                .HasDatabaseName("IX_CourseChapters_CourseId_OrderIndex");
            
            // 注意：外键关系暂不配置，等Terence完成Course实体后再由他配置
            // 或者在迁移时手动添加外键约束
        });
    }
}
