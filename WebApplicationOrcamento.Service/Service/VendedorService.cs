using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Service.Service
{
    public class VendedorService : BaseService<Vendedor>, IVendedorService
    {
        private readonly IBaseRepository<Vendedor> _repository;
        public VendedorService(IBaseRepository<Vendedor> repository) : base(repository)
        {
            _repository = repository;
        }

        public VendedorResponse CalculaComissao(Vendedor vendedor)
        {
            var valor = _repository.GetValorTotal(vendedor.Id);

            VendedorResponse vendedorResponse = new(valor)
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome
            };
            return vendedorResponse;
        }
    }
}
