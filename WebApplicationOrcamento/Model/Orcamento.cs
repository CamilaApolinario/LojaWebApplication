namespace WebApplicationOrcamento.Model
{
    public class Orcamento
    {
        public Orcamento()
        {

        }
        public Orcamento(Vendedor vendedor, Produto produto, int quantidadeProduto)
        {
            Vendedor = vendedor;
            Produto = produto;
            QuantidadeProduto = quantidadeProduto;
            ValorTotal = produto.Valor * quantidadeProduto;

        }

        public int Id { get; set; }
        public Vendedor Vendedor { get; set; }
        public Produto Produto { get; set; }
        public int QuantidadeProduto { get; set; }
        public double ValorTotal { get; set; }
        public int VendedorId { get; set; }
        public int ProdutoId { get; set; }
    }
}

