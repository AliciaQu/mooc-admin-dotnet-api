using AutoMapper.Internal.Mappers;
using Mooc.Application.Contracts;

namespace Mooc.Application;

public abstract class UpdateService<TEntity, TGetOutputDto, TKey, TUpdateInput> : ReadOnlyService<TEntity, TGetOutputDto, TKey>, IUpdateService<TGetOutputDto, TKey, TUpdateInput>
    where TEntity : BaseEntity
{
    protected UpdateService()
    {
    }
    public virtual async Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
    {
        var entity = await GetEntityByIdAsync(id);
        MapToEntity(input, entity);
        var dbSet = GetDbSet();
        if (dbSet.Local.All(e => e != entity))
        {
            dbSet.Attach(entity);
            dbSet.Update(entity);
        }
        await McDBContext.SaveChangesAsync();

        return MapToGetOutputDto(entity);
    }

    /// <summary>
    /// Maps <typeparamref name="TUpdateInput"/> to <typeparamref name="TEntity"/> to update the entity.
    /// It uses <see cref="Mapper"/> by default.
    /// It can be overriden for custom mapping.
    /// </summary>
    protected virtual void MapToEntity(TUpdateInput updateInput, TEntity entity)
    {
        this.Mapper.Map(updateInput, entity);
    }
}
