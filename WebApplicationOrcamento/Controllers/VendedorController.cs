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

        public VendedorController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetVendedor()
        {
            var vendedor = _context.Vendedor.ToList();
            return Ok(vendedor);
        }

        [HttpGet("{id}")]
        public IActionResult GetVendedor(int id)
        {
            var vendedor = _context.Vendedor.FirstOrDefault(x => x.Id == id);
            if (vendedor != null)
            {
                var orcamento = _context.Orcamento;
                var query = from Orcamento in orcamento
                            where Orcamento.Vendedor.Id == id
                            select Orcamento.ValorTotal;
                var valor = query.Sum();

                VendedorResponse vendedorResponse = new(valor)
                {
                    Id = id,
                    Nome = vendedor.Nome
                };

                return Ok(vendedorResponse);
            }
            return NotFound($"Vendedor com id: {id}, não existe");
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

        [HttpDelete]
        public IActionResult DeletaVendedor([FromQuery] string nome)
        {
            var vendedor = _context.Vendedor.FirstOrDefault(x => x.Nome == nome);
            if (vendedor == null)
            {
                return NotFound($"Vendedor {vendedor} não encontrado!");

            }
            _context.Remove(vendedor);
            _context.SaveChanges();
            return Ok();
        }
    }
}
