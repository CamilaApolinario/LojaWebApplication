using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class UpdateOrcamentoRequest
    {
        public int Quantidade { get; set; }
        public Produto? Produto { get; set; }
        public Vendedor? Vendedor { get; set; }
    }
}
