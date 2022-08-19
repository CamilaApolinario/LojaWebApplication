using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IOrcamentoService : IBaseService<Orcamento> 
    {
        Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto);

        Orcamento AtualizaOrcamento(int id, UpdateOrcamentoRequest request);

        Orcamento AtualizaQuantidadeOrcamento(int id, int quantidade);

        IList<Orcamento> BuscarTodos();
        Orcamento BuscarPorId(int id);

    }
}