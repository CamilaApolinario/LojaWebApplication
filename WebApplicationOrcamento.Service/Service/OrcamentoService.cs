using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Model;
using WebApplicationOrcamento.Service.Service;
using WebApplicationOrcamento.Domain.Interfaces;

namespace WebApplicationOrcamento
{
    public class OrcamentoService : BaseService<Orcamento>, IOrcamentoService
    {
        private readonly IBaseRepository<Orcamento> _orcamentoRepository;
        private readonly IBaseRepository<Produto> _produtoRepository;
        private readonly IBaseRepository<Vendedor> _vendedorRepository;

        public OrcamentoService(IBaseRepository<Orcamento> orcamentoRepository, 
                                IBaseRepository<Produto> produtoRepository,
                                IBaseRepository<Vendedor> vendedorRepository) : base(orcamentoRepository)
        {
            _orcamentoRepository = orcamentoRepository;
            _produtoRepository = produtoRepository;
            _vendedorRepository = vendedorRepository;
        }

        public Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto)
        {
            var orcamento = new Orcamento(vendedor, produto, quantidadeProduto);
            _orcamentoRepository.Insert(orcamento);
            return orcamento;
        }
      
        public Orcamento AtualizaOrcamento(int id, UpdateOrcamentoRequest request)
        {
            var orcamento = _orcamentoRepository.SelectId(id);
            var produto = _produtoRepository.SelectId(request.Produto.Id);
            var vendedor = _vendedorRepository.SelectId(request.Vendedor.Id);
            var valorTotal = request.Quantidade * produto.Valor;
            orcamento.ValorTotal = valorTotal;
            orcamento.QuantidadeProduto = request.Quantidade;
            orcamento.Produto = produto;
            orcamento.Vendedor = vendedor;
            _orcamentoRepository.Update(orcamento);

            return orcamento;  
        }

        public Orcamento AtualizaQuantidadeOrcamento(int id, int quantidade)
        {
            var orcamento = _orcamentoRepository.SelectId(id);
            var valorTotal = quantidade * orcamento.Produto.Valor;
            orcamento.ValorTotal = valorTotal;
            orcamento.QuantidadeProduto = quantidade;

            _orcamentoRepository.Update(orcamento);
            return orcamento;
        }      
    }
}
