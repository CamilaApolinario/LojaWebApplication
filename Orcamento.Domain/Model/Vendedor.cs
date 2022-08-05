using WebApplicationOrcamento.Domain.Entities;

namespace WebApplicationOrcamento.Model
{
    public class Vendedor : BaseEntity
    {
        public Vendedor()
        {
        }
        public Vendedor(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public string Nome { get; set; }
    }
}
