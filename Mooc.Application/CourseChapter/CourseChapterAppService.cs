using Mooc.Application.Contracts;
using Mooc.Application.Contracts.CourseChapter;
using Mooc.Application.Contracts.CourseChapter.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mooc.Application.CourseChapter;

/// <summary>
/// Course Chapter Application Service Implementation
/// Provides CRUD operations and business logic for course chapters
/// </summary>
public class CourseChapterAppService : 
    CrudService<Model.Entity.CourseChapter.CourseChapter, 
                CourseChapterOutputDto, 
                CourseChapterOutputDto, 
                long, 
                FilterPagedResultRequestDto, 
                CreateCourseChapterInputDto, 
                UpdateCourseChapterInputDto>, 
    ICourseChapterAppService, 
    ITransientDependency
{
    public CourseChapterAppService()
    {
    }

    #region Override Base CRUD Methods

    /// <summary>
    /// Create filtered query with search and sorting
    /// Supports searching by chapter name or description
    /// Default sorting: by course ID, then by order index
    /// </summary>
    protected override IQueryable<Model.Entity.CourseChapter.CourseChapter> CreateFilteredQuery(
        FilterPagedResultRequestDto input)
    {
        var query = base.CreateFilteredQuery(input);

        // Apply search filter if provided
        if (input != null && !string.IsNullOrWhiteSpace(input.Filter))
        {
            query = query.Where(x => 
                x.ChapterName.Contains(input.Filter) || 
                (x.Description != null && x.Description.Contains(input.Filter)));
        }

        // Apply default sorting
        return query.OrderBy(x => x.CourseId).ThenBy(x => x.OrderIndex);
    }

    /// <summary>
    /// Pre-processing before creating entity
    /// Sets audit fields: CreatedAt and CreatedBy
    /// </summary>
    protected override Model.Entity.CourseChapter.CourseChapter MapToEntity(
        CreateCourseChapterInputDto createInput)
    {
        // Use AutoMapper to convert DTO to Entity
        var entity = base.MapToEntity(createInput);
        
        // Set creation timestamp
        entity.CreatedAt = DateTime.Now;
        
        // TODO: Get current user ID from authentication context
        // For now, using hardcoded value
        entity.CreatedBy = 1; 
        
        return entity;
    }

    /// <summary>
    /// Pre-processing before updating entity
    /// Sets audit fields: UpdatedAt and UpdatedBy
    /// </summary>
    protected override void MapToEntity(
        UpdateCourseChapterInputDto updateInput, 
        Model.Entity.CourseChapter.CourseChapter entity)
    {
        // Use AutoMapper to update entity properties
        base.MapToEntity(updateInput, entity);
        
        // Set update timestamp
        entity.UpdatedAt = DateTime.Now;
        
        // TODO: Get current user ID from authentication context
        entity.UpdatedBy = 1;
    }

    #endregion

    #region Custom Business Methods

    /// <summary>
    /// Get all chapters for a specific course
    /// Returns chapters ordered by OrderIndex
    /// </summary>
    /// <param name="courseId">Course ID</param>
    /// <returns>List of chapter DTOs</returns>
    public async Task<List<CourseChapterOutputDto>> GetChaptersByCourseIdAsync(long courseId)
    {
        // Get queryable from repository
        var queryable = GetDbSet().AsQueryable();
        
        // Filter by course ID and order by index
        var chapters = await queryable
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.OrderIndex)
            .ToListAsync();
        
        // Use AutoMapper for batch conversion
        return Mapper.Map<List<CourseChapterOutputDto>>(chapters);
    }

    /// <summary>
    /// Update the order index of a chapter
    /// Only updates the OrderIndex field
    /// </summary>
    /// <param name="chapterId">Chapter ID</param>
    /// <param name="newOrder">New order index value</param>
    public async Task UpdateChapterOrderAsync(long chapterId, int newOrder)
    {
        // Retrieve the chapter entity
        var chapter = await GetEntityByIdAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"Chapter not found with ID: {chapterId}");
        }

        // Update order index
        chapter.OrderIndex = newOrder;
        chapter.UpdatedAt = DateTime.Now;
        
        // TODO: Get current user ID from authentication context
        chapter.UpdatedBy = 1;

        // Save changes to database
        var dbSet = GetDbSet();
        if (dbSet.Local.All(e => e != chapter))
        {
            dbSet.Attach(chapter);
        }
        dbSet.Update(chapter);
        await McDBContext.SaveChangesAsync();
    }

    /// <summary>
    /// Toggle chapter active status
    /// Switches IsActive between true and false
    /// </summary>
    /// <param name="chapterId">Chapter ID</param>
    public async Task ToggleChapterStatusAsync(long chapterId)
    {
        // Retrieve the chapter entity
        var chapter = await GetEntityByIdAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"Chapter not found with ID: {chapterId}");
        }

        // Toggle the active status
        chapter.IsActive = !chapter.IsActive;
        chapter.UpdatedAt = DateTime.Now;
        
        // TODO: Get current user ID from authentication context
        chapter.UpdatedBy = 1;

        // Save changes to database
        var dbSet = GetDbSet();
        if (dbSet.Local.All(e => e != chapter))
        {
            dbSet.Attach(chapter);
        }
        dbSet.Update(chapter);
        await McDBContext.SaveChangesAsync();
    }

    #endregion
}