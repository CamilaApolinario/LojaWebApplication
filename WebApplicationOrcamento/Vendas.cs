using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class Vendas
    {
        public List<Orcamento> Comissao  { get; set; } = new List<Orcamento>();

        public VendedorResponse VendedorResponse { get; set; }  
        
    }
    
}
