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
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<TEntity>().Remove(Select(id));
            _context.SaveChanges();
        }

        public IList<TEntity> Select() =>
            _context.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);      

    }

}


