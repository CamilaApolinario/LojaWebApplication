using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Infra.Data.Repository;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly OrcamentoRepository _orcamentoRepository;
        private readonly BaseRepository<Produto> _produtoRepository;
        private readonly BaseRepository<Vendedor> _vendedorRepository;

        public OrcamentoService(OrcamentoRepository orcamentoRepository,
                                BaseRepository<Produto> produtoRepository,
                                BaseRepository<Vendedor> vendedorRepository)
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
       
        public Orcamento GetOrcamento(int id)
        {
           return _orcamentoRepository.Select(id);
        }
      
        public Orcamento UpdateOrcamento(int id, UpdateOrcamentoRequest request)
        {
            var orcamento = _orcamentoRepository.Select(id);
            var produto = _produtoRepository.SelectName(request.Produto.Nome);
            var vendedor = _vendedorRepository.SelectName(request.Vendedor.Nome);
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
            var orcamento = _orcamentoRepository.Select(id);
            var valorTotal = quantidade * orcamento.Produto.Valor;
            orcamento.ValorTotal = valorTotal;
            orcamento.QuantidadeProduto = quantidade;

            _orcamentoRepository.Update(orcamento);
            return orcamento;
        }

        public List<Orcamento> GetOrcamento()
        {
            return _orcamentoRepository.Select().ToList();
        }

        public Orcamento DeletaOrcamento(int id)
        {
            _orcamentoRepository.Delete(id);
            return null;
        }    
    }
}
