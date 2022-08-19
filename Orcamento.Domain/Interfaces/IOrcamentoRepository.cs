using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IOrcamentoRepository : IBaseRepository<Orcamento>
    {
        IList<Orcamento> SelectAll();
        Orcamento SelectId(int id);
    }
}