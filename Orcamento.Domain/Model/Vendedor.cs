namespace WebApplicationOrcamento.Model
{
    public class Vendedor
    {
        public Vendedor()
        {
        }
        public Vendedor(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
