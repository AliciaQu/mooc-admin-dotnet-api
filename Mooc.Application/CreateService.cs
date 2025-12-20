using Mooc.Application.Contracts;
using Mooc.Core.Utils;

namespace Mooc.Application;

public abstract class CreateService<TEntity, TGetOutputDto, TCreateInput, TKey> : ReadOnlyService<TEntity, TGetOutputDto, TKey>,
    ICreateService<TGetOutputDto, TCreateInput>
    where TEntity : BaseEntity
{
    protected CreateService()
    {

    }

    public async virtual Task<TGetOutputDto> CreateAsync(TCreateInput input)
    {
        var entity = MapToEntity(input);
        GetDbSet().Add(entity);
        await this.McDBContext.SaveChangesAsync();
        return MapToGetOutputDto(entity);
    }

    /// <summary>
    /// Maps <typeparamref name="TCreateInput"/> to <typeparamref name="TEntity"/> to create a new entity.
    /// It uses <see cref="IObjectMapper"/> by default.
    /// It can be overriden for custom mapping.
    /// </summary>
    protected virtual TEntity MapToEntity(TCreateInput createInput)
    {
        var entity = this.Mapper.Map<TCreateInput, TEntity>(createInput);
        SetIdForLong(entity);
        return entity;
    }
    /// <summary>
    /// Sets Id value for the entity if <typeparamref name="TKey"/> is <see cref="long"/>.
    /// It's used while creating a new entity.
    /// </summary>
    protected virtual void SetIdForLong(TEntity entity)
    {
        if (entity is BaseEntity baseEntity && baseEntity.Id == 0)
        {
            baseEntity.Id = SnowflakeIdGeneratorUtil.NextId();
        }
    }
}
