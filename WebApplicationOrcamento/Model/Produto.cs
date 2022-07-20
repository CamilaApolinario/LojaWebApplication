namespace WebApplicationOrcamento.Model
{
    public class Produto
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

        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}
