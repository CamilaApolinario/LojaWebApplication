using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProdutoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> MostraTodosProdutos()
        {
            var produto = _context.Produto;
            return Ok(produto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MostraProdutoId(int id)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Id == id);
            
            if (produto != null)
            {
                return Ok(produto);
            }
            return NotFound($"Produto de id: {id}, não existe!");
        }

        [HttpPost]
        public IActionResult AdicionaProduto([FromBody] Produto produto)
        {
            if (produto != null)
            {
                _context.Produto.Add(produto);
                _context.SaveChanges();
                return Ok(produto);
            }
            return NotFound($"Produto: {produto} não foi adicionado");
        }

        [HttpPut]
        public IActionResult AtualizaProduto(int id, [FromBody] ProdutoUpdate novoProduto)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Id == id);
            produto.Nome= novoProduto.Nome;
            produto.Valor= novoProduto.Valor;
            _context.Produto.Update(produto);
            _context.SaveChanges();
            return Ok(novoProduto);
        }

        [HttpDelete]
        public IActionResult DeletaProduto([FromQuery] string nome)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Nome == nome);
            if (produto == null)
            {
                return NotFound($"Produto {produto} não encontrado!");
            }

            _context.Produto.Remove(produto);
            _context.SaveChanges();
            return Ok($"Produto {produto} foi excluido!");
        }
    }
}
