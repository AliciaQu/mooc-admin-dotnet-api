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
}
