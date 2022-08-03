using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces;

public interface IBaseRepository
{
    void Delete(int id);
    void Insert(Orcamento orc);
    IList<Orcamento> Select();
    Orcamento Select(int id);
    void Update(Orcamento obj);
}
