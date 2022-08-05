using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Infra.Data.Repository
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        protected readonly ApplicationContext _context;

        public OrcamentoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Insert(Orcamento orc)
        {
            _context.Set<Orcamento>().Add(orc);
            _context.SaveChanges();
        }

        public void Update(Orcamento orc)
        {
            _context.Entry(orc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<Orcamento>().Remove(Select(id));
            _context.SaveChanges();
        }

        public IList<Orcamento> Select() =>
            _context.Set<Orcamento>()
            .Include(p => p.Produto)
            .Include(v => v.Vendedor)
            .ToList();

        public Orcamento Select(int id) =>
            _context.Set<Orcamento>()
            .Include(p => p.Produto)
            .Include(v => v.Vendedor)
            .FirstOrDefault(x => x.Id == id);
    }
}
