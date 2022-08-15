using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Adicionar(TEntity obj);

        void Excluir(TEntity obj);

        IList<TEntity> BuscarTodos();

        TEntity BuscarPorId(int id);

        TEntity Atualizar(TEntity obj);        
    }
}