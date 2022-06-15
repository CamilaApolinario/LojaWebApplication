using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ApplicationContext _context;

        public ProdutoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduto()
        {
            var produto = _context.Produto;
            return Ok(produto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto(int id)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Id == id);
            return Ok(produto);
        }
        
        [HttpPost]
        public IActionResult AdicionaProduto([FromBody] Produto produto)
        {
            
            _context.Add(produto);
            _context.SaveChanges();
            if (produto != null)
            {
                return Ok(produto);
            }
            return NotFound();
        }
    }
}
