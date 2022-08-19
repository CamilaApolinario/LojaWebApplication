using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Produto BuscarPorNome(string nome);
    }
}