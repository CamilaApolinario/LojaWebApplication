using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Infra.Data.Repository
{
    public class VendedorRepository : BaseRepository<Vendedor>, IVendedorRepository
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        public VendedorRepository(ApplicationContext context, IOrcamentoRepository orcamentoRepository) : base(context)
        {
            _orcamentoRepository = orcamentoRepository;
        }
        public double ValorTotalOrcamentos(int id)
        {
            var orcamento = _orcamentoRepository.SelectAll();

            var query = from Orcamento in orcamento
                        where Orcamento.Vendedor.Id == id
                        select Orcamento.ValorTotal;
            return query.Sum();
        }
        public Vendedor BuscarPorNome(string nome)
        {
            var vendedor = _context.Vendedor.FirstOrDefault(x => x.Nome == nome);
            return vendedor;
        }

    }
}
