using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain.Interfaces
{
    public interface IVendedorService: IBaseService<Vendedor>
    {
        VendedorResponse CalculaComissao(Vendedor vendedor);
    }
}