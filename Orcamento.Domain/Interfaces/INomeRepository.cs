using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface INomeRepository<TEntity> where TEntity : BaseEntityName
    {
        TEntity SelectName(string nome);
    }
}