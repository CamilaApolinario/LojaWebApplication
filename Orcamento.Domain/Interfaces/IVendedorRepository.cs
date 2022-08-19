using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IVendedorRepository : IBaseRepository<Vendedor>
    {
        double ValorTotalOrcamentos(int id);
        Vendedor BuscarPorNome(string nome);
    }
}
