using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
    }
}