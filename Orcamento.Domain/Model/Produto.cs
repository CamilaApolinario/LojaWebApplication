using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Model
{
    public class Produto : BaseEntity
    {
        public Produto()
        {
        }
        public Produto(int id, string nome, double valor)
        {
            Id = id;
            Nome = nome;
            Valor = valor;
        }

        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}
