using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class OrcamentoService
    {
        public Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto)
        {
            var orcamento = new Orcamento(vendedor, produto, quantidadeProduto);
            return orcamento;
        }
    }
}
