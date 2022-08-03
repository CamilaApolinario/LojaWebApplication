using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IOrcamentoService
    {
        Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto);
    }
}