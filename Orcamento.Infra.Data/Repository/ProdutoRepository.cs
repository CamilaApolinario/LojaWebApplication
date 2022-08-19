using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Infra.Data.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext context) : base(context)
        {
        }
        public Produto BuscarPorNome(string nome)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Nome == nome);
            return produto;
        }
    }
}
