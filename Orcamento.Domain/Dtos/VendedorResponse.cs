namespace WebApplicationOrcamento.Domain
{
    public class VendedorResponse
    {
        public VendedorResponse()
        {
        }

        public VendedorResponse(double valorTotalOrcamento)
        {
            Comissao = valorTotalOrcamento * 0.2;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Comissao { get; set; }
    }
}