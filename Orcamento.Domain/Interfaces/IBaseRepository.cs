namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> 
    {
        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(TEntity obj);

        IList<TEntity> SelectAll();

        TEntity SelectId(int id);     
    }
}