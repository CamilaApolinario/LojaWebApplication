using WebApplicationOrcamento.Domain.Interfaces;

namespace WebApplicationOrcamento.Model
{
    public class Orcamento : Recurso, IBaseEntity
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

        public int Id { get; set; }
        public virtual Vendedor Vendedor { get; set; }      
        public virtual Produto Produto { get; set; }
        public int QuantidadeProduto { get; set; }
        public double ValorTotal { get; set;}
    }
}

