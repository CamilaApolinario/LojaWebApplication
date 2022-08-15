using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Infra.Data.Repository
{
    public class NomeRepository<TEntity> : INomeRepository<TEntity> where TEntity : BaseEntityName
    {
        private readonly ApplicationContext _context;

        public NomeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public TEntity SelectName(string nome) =>
            _context.Set<TEntity>().FirstOrDefault(x => x.Nome == nome);
    }
}
