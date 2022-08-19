using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Service.Service
{
    public class VendedorService : BaseService<Vendedor>, IVendedorService
    {
        private readonly IVendedorRepository _repository;
        public VendedorService(IVendedorRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public VendedorResponse CalculaComissao(Vendedor vendedor)
        {
            var valor = _repository.ValorTotalOrcamentos(vendedor.Id);

            VendedorResponse vendedorResponse = new(valor)
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome
            };
            return vendedorResponse;
        }
        public Vendedor BuscarPorNome(string nome) => _repository.BuscarPorNome(nome);
    }
}
