using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Infra.Data.Repository
{

    public class OrcamentoRepository : IBaseRepository
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

        public void Update(Orcamento obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<Orcamento>().Remove(Select(id));
            _context.SaveChanges();
        }

        public IList<Orcamento> Select() =>
            _context.Set<Orcamento>().ToList();

        public Orcamento Select(int id) =>
            _context.Set<Orcamento>().Find(id);

    }
}

