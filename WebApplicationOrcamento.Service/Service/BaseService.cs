using WebApplicationOrcamento.Domain.Entities;
using WebApplicationOrcamento.Domain.Interfaces;

namespace WebApplicationOrcamento.Service.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public TEntity Adicionar(TEntity obj)
        {
            _baseRepository.Insert(obj);
            return obj;
        }

        public void Excluir(TEntity obj) => _baseRepository.Delete(obj);

        public IList<TEntity> BuscarTodos() => _baseRepository.SelectAll().ToList();

        public TEntity BuscarPorId(int id) => _baseRepository.SelectId(id);

        public TEntity Atualizar(TEntity obj)
        {
            _baseRepository.Update(obj);
            return obj;
        }        
    }
}
