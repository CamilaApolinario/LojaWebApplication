using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly OrcamentoService _orcamentoService;


        public VendedorController(ApplicationContext context, OrcamentoService orcamentoService)
        {
            _context = context;
            _orcamentoService = orcamentoService;

        }
        [HttpGet]
        public async Task<IActionResult> GetVendedor()
        {
            var vendedor = _context.Vendedor;
            return Ok(vendedor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendedor(int id)
        {
            var vendedor = _context.Vendedor.FirstOrDefault(x => x.Id == id);
            if (vendedor != null)
            {
                
                var orcamento = _context.Orcamento;
                var query = from Orcamento in orcamento
                            where Orcamento.VendedorId == id
                            select Orcamento.ValorTotal;
                var valor = query.Sum();

                VendedorResponse vendedorResponse = new(valor);
                vendedorResponse.Id = id;
                vendedorResponse.Nome = vendedor.Nome;
                
                return Ok(vendedorResponse);

            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AdicionaVendedor([FromBody] Vendedor vendedor)
        {
            if (vendedor != null)
            {
                _context.Add(vendedor);
                _context.SaveChanges();
                return Ok(vendedor);
            }
            return NotFound();
        }
    }
}
