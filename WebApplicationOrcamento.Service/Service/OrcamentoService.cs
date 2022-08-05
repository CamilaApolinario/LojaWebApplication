using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Infra.Data.Repository;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _baseRepository;
        private readonly ApplicationContext _context;
        private readonly BaseRepository<Produto> _produtoRepository;
        private readonly BaseRepository<Vendedor> _vendedorRepository;

        public OrcamentoService(IOrcamentoRepository baseRepository, ApplicationContext context, BaseRepository<Produto> produtoRepository, BaseRepository<Vendedor> vendedorRepository)
        {
            _baseRepository = baseRepository;
            _context = context;
            _produtoRepository = produtoRepository;
            _vendedorRepository = vendedorRepository;
        }

        public Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto)
        {
            var orcamento = new Orcamento(vendedor, produto, quantidadeProduto);
            _baseRepository.Insert(orcamento);
            return orcamento;
        }

       

        public Orcamento GetOrcamento(int id)
        {
           return _baseRepository.Select(id);
        }

       
        public Orcamento UpdateOrcamento(UpdateOrcamentoRequest request)
        {
            var orcamento = _baseRepository.Select(request.Id);
            var produto = _produtoRepository.Select(request.Produto.Id);
            var vendedor = _vendedorRepository.Select(request.Vendedor.Id);
            var valorTotal = request.Quantidade * produto.Valor;
            orcamento.ValorTotal = valorTotal;
            orcamento.QuantidadeProduto = request.Quantidade;
            orcamento.Produto = produto;
            orcamento.Vendedor = vendedor;
            _baseRepository.Update(orcamento);
            return orcamento;
        }

        public  List<Orcamento> GetOrcamento()
        {
            return _baseRepository.Select().ToList();
        }

        public Orcamento DeletaOrcamento(int id)
        {
            _baseRepository.Delete(id);
            return null;
        }    
    }
}
