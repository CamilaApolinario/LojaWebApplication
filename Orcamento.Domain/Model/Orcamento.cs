using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Model
{
    public class Orcamento : BaseEntity
    {
        public Orcamento()
        {
        }

        public Orcamento(Vendedor vendedor, Produto produto, int quantidadeProduto)
        {
            Vendedor = vendedor;
            Produto = produto;
            QuantidadeProduto = quantidadeProduto;
            ValorTotal = quantidadeProduto * produto.Valor;
        }

        public virtual Vendedor Vendedor { get; set; }      
        public virtual Produto Produto { get; set; }
        public int QuantidadeProduto { get; set; }
        public double ValorTotal { get; set;}
    }
}

