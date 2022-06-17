using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class OrcamentoService
    {
        private readonly ApplicationContext _context;
        public OrcamentoService(ApplicationContext context)
        {
            _context = context;
        }

        public Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto)
        {
            var orcamento = new Orcamento(vendedor, produto, quantidadeProduto);
            return orcamento;
        }

    }
}
