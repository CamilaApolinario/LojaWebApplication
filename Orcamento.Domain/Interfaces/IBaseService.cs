using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
    }
}