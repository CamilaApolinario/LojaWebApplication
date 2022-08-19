using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Model;
using WebApplicationOrcamento.Service.Service;
using WebApplicationOrcamento.Domain.Interfaces;

namespace WebApplicationOrcamento
{
    public class OrcamentoService : BaseService<Orcamento>, IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IVendedorRepository _vendedorRepository;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository, 
                                IProdutoRepository produtoRepository,
                                IVendedorRepository vendedorRepository) : base(orcamentoRepository)
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
            var produto = _produtoRepository.BuscarPorNome(request.Produto.Nome);
            var vendedor = _vendedorRepository.BuscarPorNome(request.Vendedor.Nome);
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

        public IList<Orcamento> BuscarTodos() => _orcamentoRepository.SelectAll();
        public Orcamento BuscarPorId(int id) => _orcamentoRepository.SelectId(id);
    }
}
