using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Service.Service
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _repository;
        public ProdutoService(IProdutoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Produto BuscarPorNome(string nome) => _repository.BuscarPorNome(nome);
    }
}
