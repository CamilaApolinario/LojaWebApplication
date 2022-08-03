using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IBaseRepository _baseRepository;

        public OrcamentoService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto)
        {
            var orcamento = new Orcamento(vendedor, produto, quantidadeProduto);
            _baseRepository.Insert(orcamento);
            return orcamento;
        }
    }
}
