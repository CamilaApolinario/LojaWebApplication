using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Domain
{
    public class UpdateOrcamentoRequest
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public Produto? Produto { get; set; }
        public Vendedor? Vendedor { get; set; }
    }
}
