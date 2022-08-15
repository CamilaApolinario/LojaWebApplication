using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IProdutoService : IBaseService<Produto>
    {
        Produto BuscarPorNome(string nome);
    }
}