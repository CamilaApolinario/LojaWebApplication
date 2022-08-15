using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Infra.Data.Repository
{
    public class OrcamentoRepository : BaseRepository<Orcamento>, IBaseRepository<Orcamento> 
    {
        //protected readonly ApplicationContext _context;

        public OrcamentoRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Orcamento> Select() =>
            _context.Set<Orcamento>()
            .Include(p => p.Produto)
            .Include(v => v.Vendedor)
            .ToList();

        public Orcamento? SelectId(int id) =>
            _context.Set<Orcamento>()
            .Include(p => p.Produto)
            .Include(v => v.Vendedor)
            .FirstOrDefault(x => x.Id == id);
    }
}
