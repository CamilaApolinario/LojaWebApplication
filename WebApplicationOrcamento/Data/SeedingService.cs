using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Data
{
    public class SeedingService
    {
        private ApplicationContext _context;
        public SeedingService(ApplicationContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Orcamento.Any() ||
                _context.Vendedor.Any() ||
                _context.Produto.Any())
            {
                return; // DB has been seeded
            }

            Produto p1 = new(1, "Borracha", 1.5);
            Produto p2 = new(2, "Caneta", 2.8);
            Produto p3 = new(3, "Lapiseira", 5.9);
            Produto p4 = new(4, "Sulfite", 10.99);
            Produto p5 = new(5, "Grampeador", 3.59);



            Vendedor v1 = new(1, "João");
            Vendedor v2 = new(1, "Maria");
            Vendedor v3 = new(1, "Olivia");
            Vendedor v4 = new(1, "Pedro");
            Vendedor v5 = new(1, "Carol");



            Orcamento o1 = new(v1, p1, 2);
            Orcamento o2 = new(v3, p4, 5);


            _context.Produto.AddRange(p1, p2, p3, p4, p5);

            _context.Vendedor.AddRange(v1, v2, v3, v4, v5);

            _context.Orcamento.AddRange(o1, o2);

            _context.SaveChanges();
        }
    }
}
