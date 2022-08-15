using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Entities;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext _context;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity obj)
        {
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public IList<TEntity> SelectAll() =>
            _context.Set<TEntity>().ToList();   

        public TEntity SelectId(int id) =>
             _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);

        public double GetValorTotal(int id)
        {
            var orcamento = _context.Orcamento.ToList();

            var query = from Orcamento in orcamento
            where Orcamento.Vendedor.Id == id
            select Orcamento.ValorTotal;
            return query.Sum();
        }
    }
}


