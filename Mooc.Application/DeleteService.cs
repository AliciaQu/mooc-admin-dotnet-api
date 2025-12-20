using Mooc.Application.Contracts;
namespace Mooc.Application;

public abstract class DeleteService<TEntity, TKey> : IDeleteService<TKey>
    where TEntity : BaseEntity
{
    protected MoocDBContext BDContext { get; }
    protected IMapper Mapper { get; }
    public DeleteService(MoocDBContext dbContext, IMapper mapper)
    {
        this.BDContext = dbContext;
        this.Mapper = mapper;
    }
    public virtual async Task DeleteAsync(TKey id)
    {
        var dbSet = this.BDContext.Set<TEntity>();
        var entity = await dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (entity != null)
        {
            dbSet.Remove(entity);
            await BDContext.SaveChangesAsync();
        }
    }
}
